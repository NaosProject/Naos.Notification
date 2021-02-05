// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleProcessSendNotificationSagaProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Protocol.Bot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Naos.Database.Domain;
    using Naos.Notification.Domain;
    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Executes a <see cref="ProcessSendNotificationSagaOp"/>.
    /// </summary>
    public class HandleProcessSendNotificationSagaProtocol : HandleRecordAsyncSpecificProtocolBase<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>>
    {
        private readonly IWriteOnlyStream notificationEventStream;

        private readonly IReadOnlyDictionary<IDeliveryChannel, IReadOnlyStream> channelToEventStreamMap;

        private readonly IBuildTagsProtocol<AttemptedToSendNotificationEvent> buildAttemptedToSendNotificationEventTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleProcessSendNotificationSagaProtocol"/> class.
        /// </summary>
        /// <param name="notificationEventStream">The event stream.</param>
        /// <param name="channelToEventStreamMap">A map of delivery channel to the channel's event stream.</param>
        /// <param name="buildAttemptedToSendNotificationEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="AttemptedToSendNotificationEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        public HandleProcessSendNotificationSagaProtocol(
            IWriteOnlyStream notificationEventStream,
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyStream> channelToEventStreamMap,
            IBuildTagsProtocol<AttemptedToSendNotificationEvent> buildAttemptedToSendNotificationEventTags = null)
        {
            new { notificationEventStream }.AsArg().Must().NotBeNull();
            new { channelToEventStreamMap }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();

            this.notificationEventStream = notificationEventStream;
            this.channelToEventStreamMap = channelToEventStreamMap;
            this.buildAttemptedToSendNotificationEventTags = buildAttemptedToSendNotificationEventTags;
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(
            HandleRecordOp<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            // Pull some info out of the operation.
            var processSendNotificationSagaOp = operation.RecordToHandle.Payload.Operation;

            var inheritableTags = operation.RecordToHandle.Metadata.Tags?.ToDictionary(_ => _.Key, _ => _.Value);

            // Poll for failure event
            var channels = processSendNotificationSagaOp.ChannelToOperationOutcomeSpecsMap.Keys;

            var succeededChannels = new List<IDeliveryChannel>();

            var failedChannels = new List<IDeliveryChannel>();

            foreach (var channel in channels)
            {
                new { this.channelToEventStreamMap }.AsOp().Must().ContainKey(channel, Invariant($"Looking for success/failure events on event stream for chanel {channel.GetType().ToStringReadable()} but there is no event stream associated with that channel."));

                var eventStream = this.channelToEventStreamMap[channel];

                var operationOutcomeSpecs = processSendNotificationSagaOp.ChannelToOperationOutcomeSpecsMap[channel];

                foreach (var operationOutcomeSpec in operationOutcomeSpecs)
                {
                    var failureEventMetadata = await eventStream.GetLatestRecordMetadataByIdAsync(
                        operationOutcomeSpec.ChannelTrackingCodeId,
                        operationOutcomeSpec.FailedEventType,
                        existingRecordNotEncounteredStrategy: ExistingRecordNotEncounteredStrategy.ReturnDefault);

                    if (failureEventMetadata != null)
                    {
                        // Merge-in the tags on the failure event.
                        AddMissingTags(inheritableTags, failureEventMetadata.Tags);

                        failedChannels.Add(channel);
                    }
                    else
                    {
                        var successEventMetadata = await eventStream.GetLatestRecordMetadataByIdAsync(
                            operationOutcomeSpec.ChannelTrackingCodeId,
                            operationOutcomeSpec.SucceededEventType,
                            existingRecordNotEncounteredStrategy: ExistingRecordNotEncounteredStrategy.ReturnDefault);

                        if (successEventMetadata != null)
                        {
                            // Merge-in the tags on the success event.
                            AddMissingTags(inheritableTags, successEventMetadata.Tags);

                            succeededChannels.Add(channel);
                        }
                        else
                        {
                            // Neither succeeded nor failed, cancel the run and let this handler try again in the future.
                            throw new SelfCancelRunningExecutionException(Invariant($"Neither the success nor failure event exists for the {channel} channel."));
                        }
                    }
                }
            }

            succeededChannels = succeededChannels.Distinct().ToList();

            failedChannels = failedChannels.Distinct().ToList();

            // Write the outcome to the Notification Event Stream.
            var notificationTrackingCodeId = processSendNotificationSagaOp.NotificationTrackingCodeId;

            var attemptedToSendNotificationEvent = new AttemptedToSendNotificationEvent(notificationTrackingCodeId, DateTime.UtcNow, succeededChannels, failedChannels);

            var tags = await this.buildAttemptedToSendNotificationEventTags.ExecuteBuildTagsAsync(notificationTrackingCodeId, attemptedToSendNotificationEvent, inheritableTags);

            await this.notificationEventStream.PutWithIdAsync(notificationTrackingCodeId, attemptedToSendNotificationEvent, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundByIdAndType);
        }

        private static void AddMissingTags(
            Dictionary<string, string> tagsToAddTo,
            IReadOnlyDictionary<string, string> tagsToMergeIn)
        {
            if (tagsToMergeIn != null)
            {
                foreach (var key in tagsToMergeIn.Keys)
                {
                    if (!tagsToAddTo.ContainsKey(key))
                    {
                        tagsToAddTo.Add(key, tagsToMergeIn[key]);
                    }
                }
            }
        }
    }
}