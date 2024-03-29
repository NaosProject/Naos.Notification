﻿// --------------------------------------------------------------------------------------------------------------------
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
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Cloning.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Executes a <see cref="ProcessSendNotificationSagaOp"/>.
    /// </summary>
    public class HandleProcessSendNotificationSagaProtocol : HandleRecordAsyncSpecificProtocolBase<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>>
    {
        private readonly IWriteOnlyStream notificationEventStream;

        private readonly IReadOnlyDictionary<IDeliveryChannel, IReadOnlyStream> channelToEventStreamMap;

        private readonly IBuildTagsProtocol<AttemptToSendNotificationEventBase> buildAttemptToSendNotificationEventTagsProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleProcessSendNotificationSagaProtocol"/> class.
        /// </summary>
        /// <param name="notificationEventStream">The event stream.</param>
        /// <param name="channelToEventStreamMap">A map of delivery channel to the channel's event stream.</param>
        /// <param name="buildAttemptToSendNotificationEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="AttemptToSendNotificationEventBase"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        public HandleProcessSendNotificationSagaProtocol(
            IWriteOnlyStream notificationEventStream,
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyStream> channelToEventStreamMap,
            IBuildTagsProtocol<AttemptToSendNotificationEventBase> buildAttemptToSendNotificationEventTagsProtocol = null)
        {
            new { notificationEventStream }.AsArg().Must().NotBeNull();
            new { channelToEventStreamMap }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();

            this.notificationEventStream = notificationEventStream;
            this.channelToEventStreamMap = channelToEventStreamMap;
            this.buildAttemptToSendNotificationEventTagsProtocol = buildAttemptToSendNotificationEventTagsProtocol;
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(
            HandleRecordOp<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            // Pull some info out of the operation.
            var executeOpRequestedEvent = operation.RecordToHandle.Payload;

            var processSendNotificationSagaOp = executeOpRequestedEvent.Operation;

            var inheritableTags = (operation.RecordToHandle.Metadata.Tags?.DeepClone() ?? new List<NamedValue<string>>()).ToList();

            // Poll for failure event
            var channels = processSendNotificationSagaOp.ChannelToOperationsMonitoringInfoMap.Keys;

            var channelToOperationsOutcomeInfoMap = new Dictionary<IDeliveryChannel, IReadOnlyCollection<ChannelOperationOutcomeInfo>>();

            foreach (var channel in channels)
            {
                new { this.channelToEventStreamMap }.AsOp().Must().ContainKey(channel, Invariant($"Looking for success/failure events on event stream for chanel {channel.GetType().ToStringReadable()} but there is no event stream associated with that channel."));

                var eventStream = this.channelToEventStreamMap[channel];

                var operationsMonitoringInfo = processSendNotificationSagaOp.ChannelToOperationsMonitoringInfoMap[channel];

                var operationsOutcomeInfo = new List<ChannelOperationOutcomeInfo>();

                channelToOperationsOutcomeInfoMap.Add(channel, operationsOutcomeInfo);

                foreach (var operationMonitoringInfo in operationsMonitoringInfo)
                {
                    ChannelOperationOutcomeInfo operationOutcomeInfo;

                    var failureEventMetadata = await eventStream.GetLatestRecordMetadataByIdAsync(
                        operationMonitoringInfo.ChannelOperationTrackingCodeId,
                        operationMonitoringInfo.FailedEventType,
                        recordNotFoundStrategy: RecordNotFoundStrategy.ReturnDefault);

                    if (failureEventMetadata != null)
                    {
                        // Merge-in the tags on the failure event.
                        AddMissingTags(inheritableTags, failureEventMetadata.Tags);

                        operationOutcomeInfo = new ChannelOperationOutcomeInfo(operationMonitoringInfo.ChannelOperationTrackingCodeId, operationMonitoringInfo.FailedEventType, ChannelOperationOutcome.Failed);
                    }
                    else
                    {
                        var successEventMetadata = await eventStream.GetLatestRecordMetadataByIdAsync(
                            operationMonitoringInfo.ChannelOperationTrackingCodeId,
                            operationMonitoringInfo.SucceededEventType,
                            recordNotFoundStrategy: RecordNotFoundStrategy.ReturnDefault);

                        if (successEventMetadata != null)
                        {
                            // Merge-in the tags on the success event.
                            AddMissingTags(inheritableTags, successEventMetadata.Tags);

                            operationOutcomeInfo = new ChannelOperationOutcomeInfo(operationMonitoringInfo.ChannelOperationTrackingCodeId, operationMonitoringInfo.SucceededEventType, ChannelOperationOutcome.Succeeded);
                        }
                        else
                        {
                            // Neither succeeded nor failed, cancel the run and let this handler try again in the future.
                            throw new SelfCancelRunningExecutionException(Invariant($"Notification '{executeOpRequestedEvent.Id}' is being sent on the '{channel.GetType().ToStringReadable()}' channel via channel-operation '{operationMonitoringInfo.ChannelOperationTrackingCodeId}', however the events that indicate whether the operation succeeded ('{operationMonitoringInfo.SucceededEventType}') or failed ('{operationMonitoringInfo.FailedEventType}') do not exist in the channel event stream.  Is the operation still executing?  Cancelling the handling of this Saga operation."));
                        }
                    }

                    operationsOutcomeInfo.Add(operationOutcomeInfo);
                }
            }

            // Write the AttemptToSendNotificationEventBase to the Event Stream.
            var notificationTrackingCodeId = processSendNotificationSagaOp.NotificationTrackingCodeId;

            var attemptToSendNotificationResult = new AttemptToSendNotificationResult(channelToOperationsOutcomeInfoMap);

            var attemptToSendNotificationOutcome = attemptToSendNotificationResult.GetOutcome();

            AttemptToSendNotificationEventBase attemptToSendNotificationEvent;

            switch (attemptToSendNotificationOutcome)
            {
                case AttemptToSendNotificationOutcome.SentOnAllPreparedChannels:
                    attemptToSendNotificationEvent = new SentOnAllPreparedChannelsEvent(notificationTrackingCodeId, DateTime.UtcNow, attemptToSendNotificationResult);
                    break;
                case AttemptToSendNotificationOutcome.SentOnSomePreparedChannels:
                    attemptToSendNotificationEvent = new SentOnSomePreparedChannelsEvent(notificationTrackingCodeId, DateTime.UtcNow, attemptToSendNotificationResult);
                    break;
                case AttemptToSendNotificationOutcome.CouldNotSendOnAnyPreparedChannel:
                    attemptToSendNotificationEvent = new CouldNotSendOnAnyPreparedChannelEvent(notificationTrackingCodeId, DateTime.UtcNow, attemptToSendNotificationResult);
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(AttemptToSendNotificationOutcome)} is not supported: {attemptToSendNotificationOutcome}."));
            }

            var tags = this.buildAttemptToSendNotificationEventTagsProtocol.ExecuteBuildTags(notificationTrackingCodeId, attemptToSendNotificationEvent, inheritableTags);

            await this.notificationEventStream.PutWithIdAsync(notificationTrackingCodeId, attemptToSendNotificationEvent, tags, ExistingRecordStrategy.DoNotWriteIfFoundByIdAndType);
        }

        private static void AddMissingTags(
            ICollection<NamedValue<string>> tagsToAddTo,
            IReadOnlyCollection<NamedValue<string>> tagsToMergeIn)
        {
            if (tagsToMergeIn != null)
            {
                foreach (var tagToMergeIn in tagsToMergeIn)
                {
                    if (!tagsToAddTo.Contains(tagToMergeIn))
                    {
                        tagsToAddTo.Add(tagToMergeIn);
                    }
                }
            }
        }
    }
}