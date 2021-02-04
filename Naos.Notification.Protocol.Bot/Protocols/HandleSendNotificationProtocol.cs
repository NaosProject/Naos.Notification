// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleSendNotificationProtocol.cs" company="Naos Project">
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
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Executes a <see cref="SendNotificationOp"/>.
    /// </summary>
    public abstract class HandleSendNotificationProtocol : HandleRecordAsyncSpecificProtocolBase<ExecuteOpRequestedEvent<long, SendNotificationOp>>
    {
        private readonly IWriteOnlyStream eventStream;

        private readonly IWriteOnlyStream sagaStream;

        private readonly IGetAudience getAudienceProtocol;

        private readonly IGetDeliveryChannelConfigs getDeliveryChannelConfigsProtocol;

        private readonly IPrepareToSendOnChannel prepareToSendOnChannelProtocol;

        private readonly CannotStageToSendOnChannelAction cannotStageToSendOnChannelAction;

        private readonly IReadOnlyDictionary<IDeliveryChannel, IWriteOnlyStream> channelToOperationStreamMap;

        private readonly IBuildTagsForEvent<SendNotificationRequestedEvent> buildSendNotificationRequestedEventTags;

        private readonly IBuildTagsForEvent<CannotGetAudienceEvent> buildCannotGetAudienceEventTags;

        private readonly IBuildTagsForEvent<CannotGetDeliveryChannelConfigsEvent> buildCannotGetDeliveryChannelConfigsEventTags;

        private readonly IBuildTagsForEvent<NoChannelsToSendOnEvent> buildNoChannelsToSendOnEventTags;

        private readonly IBuildTagsForEvent<StagedToSendOnChannelsEvent> buildStagedToSendOnChannelEventTags;

        private readonly IBuildTagsForEvent<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>> buildExecuteProcessSendNotificationSagaEventTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleSendNotificationProtocol"/> class.
        /// </summary>
        /// <param name="eventStream">The Notification Event Stream.</param>
        /// <param name="sagaStream">The Notification Saga Stream.</param>
        /// <param name="getAudienceProtocol">Executes a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsProtocol">Executes a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <param name="prepareToSendOnChannelProtocol">Executes a <see cref="PrepareToSendOnChannelOp"/>.</param>
        /// <param name="cannotStageToSendOnChannelAction">Specifies what to do when we encounter a situation where we cannot stage to send on a channel.</param>
        /// <param name="channelToOperationStreamMap">A map of delivery channel to the channel's operation stream.</param>
        /// <param name="buildSendNotificationRequestedEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendNotificationRequestedEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildCannotGetAudienceEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="CannotGetAudienceEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildCannotGetDeliveryChannelConfigsEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="CannotGetDeliveryChannelConfigsEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildNoChannelsToSendOnEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="NoChannelsToSendOnEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildStagedToSendOnChannelEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="StagedToSendOnChannelsEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildExecuteProcessSendNotificationSagaEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="ProcessSendNotificationSagaOp"/> into the Notification Saga Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        protected HandleSendNotificationProtocol(
            IWriteOnlyStream eventStream,
            IWriteOnlyStream sagaStream,
            IGetAudience getAudienceProtocol,
            IGetDeliveryChannelConfigs getDeliveryChannelConfigsProtocol,
            IPrepareToSendOnChannel prepareToSendOnChannelProtocol,
            CannotStageToSendOnChannelAction cannotStageToSendOnChannelAction,
            IReadOnlyDictionary<IDeliveryChannel, IWriteOnlyStream> channelToOperationStreamMap,
            IBuildTagsForEvent<SendNotificationRequestedEvent> buildSendNotificationRequestedEventTags = null,
            IBuildTagsForEvent<CannotGetAudienceEvent> buildCannotGetAudienceEventTags = null,
            IBuildTagsForEvent<CannotGetDeliveryChannelConfigsEvent> buildCannotGetDeliveryChannelConfigsEventTags = null,
            IBuildTagsForEvent<NoChannelsToSendOnEvent> buildNoChannelsToSendOnEventTags = null,
            IBuildTagsForEvent<StagedToSendOnChannelsEvent> buildStagedToSendOnChannelEventTags = null,
            IBuildTagsForEvent<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>> buildExecuteProcessSendNotificationSagaEventTags = null)
        {
            new { eventStream }.AsArg().Must().NotBeNull();
            new { sagaStream }.AsArg().Must().NotBeNull();
            new { getAudienceProtocol }.AsArg().Must().NotBeNull();
            new { getDeliveryChannelConfigsProtocol }.AsArg().Must().NotBeNull();
            new { prepareToSendOnChannelProtocol }.AsArg().Must().NotBeNull();
            new { cannotStageToSendOnChannelAction }.AsArg().Must().NotBeEqualTo(CannotStageToSendOnChannelAction.Unknown);
            new { channelToOperationStreamMap }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();

            this.eventStream = eventStream;
            this.sagaStream = sagaStream;
            this.getAudienceProtocol = getAudienceProtocol;
            this.getDeliveryChannelConfigsProtocol = getDeliveryChannelConfigsProtocol;
            this.prepareToSendOnChannelProtocol = prepareToSendOnChannelProtocol;
            this.cannotStageToSendOnChannelAction = cannotStageToSendOnChannelAction;
            this.channelToOperationStreamMap = channelToOperationStreamMap;
            this.buildSendNotificationRequestedEventTags = buildSendNotificationRequestedEventTags;
            this.buildCannotGetAudienceEventTags = buildCannotGetAudienceEventTags;
            this.buildCannotGetDeliveryChannelConfigsEventTags = buildCannotGetDeliveryChannelConfigsEventTags;
            this.buildNoChannelsToSendOnEventTags = buildNoChannelsToSendOnEventTags;
            this.buildStagedToSendOnChannelEventTags = buildStagedToSendOnChannelEventTags;
            this.buildExecuteProcessSendNotificationSagaEventTags = buildExecuteProcessSendNotificationSagaEventTags;
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(
            HandleRecordOp<ExecuteOpRequestedEvent<long, SendNotificationOp>> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            // Pull some info out of the operation.
            var trackingCodeId = operation.RecordToHandle.Payload.Id;
            var sendNotificationOp = operation.RecordToHandle.Payload.Operation;
            var notification = sendNotificationOp.Notification;
            var tags = operation.RecordToHandle.Metadata.Tags;

            // Write SendNotificationRequestedEvent to the Event Stream (creates the Notification Aggregate).
            await this.PutSendNotificationRequestedEventAsync(sendNotificationOp, trackingCodeId, tags);

            // Get the audience and write PutNoAudienceEventAsync
            // to the Event Stream if there is none of if we should stop on failures.
            var getAudienceResult = await this.GetAudienceAsync(notification);

            if (ShouldStop(getAudienceResult))
            {
                await this.PutCannotGetAudienceEventAsync(getAudienceResult, trackingCodeId, tags);

                return;
            }

            // Get the delivery channel configs and write CannotGetDeliveryChannelConfigsEvent
            // to the Event Stream if there are none or if we should stop on failures.
            var audience = getAudienceResult.Audience;

            var getDeliveryChannelConfigsResult = await this.GetDeliveryChannelConfigsAsync(notification, getAudienceResult.Audience);

            if (ShouldStop(getDeliveryChannelConfigsResult))
            {
                await this.PutCannotGetDeliveryChannelConfigsEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, trackingCodeId, tags);

                return;
            }

            // Prepare to send on each channel and stage for sending.
            // Write NoChannelsToSendOnEventAsync if none of the channels can be staged.
            var channelConfigs = getDeliveryChannelConfigsResult.Configs;

            var channelToPrepareToSendOnChannelResultMap = new Dictionary<IDeliveryChannel, PrepareToSendOnChannelResult>();

            var channelToOperationInstructionsMap = new Dictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationInstruction>>();

            foreach (var channelConfig in channelConfigs)
            {
                if (channelConfig.Action != DeliveryChannelAction.SendOnChannel)
                {
                    continue;
                }

                var channel = channelConfig.Channel;

                var prepareToSendOnChannelResult = await this.PrepareToSendOnChannelAsync(notification, audience, channel, tags);

                channelToPrepareToSendOnChannelResultMap.Add(channel, prepareToSendOnChannelResult);

                if (ShouldStage(prepareToSendOnChannelResult))
                {
                    new { this.channelToOperationStreamMap }.AsOp().Must().ContainKey(channel, Invariant($"Staging to send on channel {channel.GetType().ToStringReadable()} but there is no operation stream associated with that channel."));

                    channelToOperationInstructionsMap.Add(channel, prepareToSendOnChannelResult.ChannelOperationInstructions);
                }
                else
                {
                    if (this.cannotStageToSendOnChannelAction == CannotStageToSendOnChannelAction.ContinueAndAttemptStagingToSendOnNextChannel)
                    {
                        // no-op
                    }
                    else if (this.cannotStageToSendOnChannelAction == CannotStageToSendOnChannelAction.StopAndNotDoNotSendOnAnyChannels)
                    {
                        await this.PutNoChannelsToSendOnEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, channelToPrepareToSendOnChannelResultMap, trackingCodeId, tags);

                        return;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(CannotStageToSendOnChannelAction)} is not supported: {this.cannotStageToSendOnChannelAction}."));
                    }
                }
            }

            if (!channelToOperationInstructionsMap.Any())
            {
                await this.PutNoChannelsToSendOnEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, channelToPrepareToSendOnChannelResultMap, trackingCodeId, tags);

                return;
            }

            // Channels are staged for sending, write StagedToSendOnChannelsEventAsync.
            await this.PutStagedToSendOnChannelsEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, channelToPrepareToSendOnChannelResultMap, channelToOperationInstructionsMap.Keys.ToList(), trackingCodeId, tags);

            // Create a Saga to track channel operations.
            await this.PutSagaAsync(trackingCodeId, channelToOperationInstructionsMap, tags);

            // Push the channel-operations to their streams.
            foreach (var channel in channelToOperationInstructionsMap.Keys)
            {
                var channelOperationInstructions = channelToOperationInstructionsMap[channel];

                await this.PutChannelOperationsAsync(channel, channelOperationInstructions);
            }
        }

        private static bool ShouldStop(
            GetAudienceResult getAudienceResult)
        {
            bool result;

            if (getAudienceResult.Audience == null)
            {
                result = true;
            }
            else
            {
                if ((getAudienceResult.Failures == null) || (!getAudienceResult.Failures.Any()))
                {
                    result = false;
                }
                else
                {
                    result = getAudienceResult.FailureAction == FailureAction.Stop;
                }
            }

            return result;
        }

        private static bool ShouldStop(
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult)
        {
            bool result;

            if ((getDeliveryChannelConfigsResult.Configs == null) || (!getDeliveryChannelConfigsResult.Configs.Any()))
            {
                result = true;
            }
            else
            {
                if ((getDeliveryChannelConfigsResult.Failures == null) || (!getDeliveryChannelConfigsResult.Failures.Any()))
                {
                    result = false;
                }
                else
                {
                    result = getDeliveryChannelConfigsResult.FailureAction == FailureAction.Stop;
                }
            }

            return result;
        }

        private static bool ShouldStage(
            PrepareToSendOnChannelResult prepareToSendOnChannelResult)
        {
            bool result;

            if ((prepareToSendOnChannelResult.ChannelOperationInstructions == null) || (!prepareToSendOnChannelResult.ChannelOperationInstructions.Any()))
            {
                result = false;
            }
            else
            {
                if ((prepareToSendOnChannelResult.Failures == null) || (!prepareToSendOnChannelResult.Failures.Any()))
                {
                    result = true;
                }
                else
                {
                    result = prepareToSendOnChannelResult.FailureAction == PrepareToSendOnChannelFailureAction.IgnoreAndStageForSendingOnChannel;
                }
            }

            return result;
        }

        private async Task PutSendNotificationRequestedEventAsync(
            SendNotificationOp sendNotificationOp,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new SendNotificationRequestedEvent(trackingCodeId, DateTime.UtcNow, sendNotificationOp);

            var tags = await this.buildSendNotificationRequestedEventTags.ExecuteBuildTagsForEventAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutCannotGetAudienceEventAsync(
            GetAudienceResult getAudienceResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new CannotGetAudienceEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult);

            var tags = await this.buildCannotGetAudienceEventTags.ExecuteBuildTagsForEventAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutCannotGetDeliveryChannelConfigsEventAsync(
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new CannotGetDeliveryChannelConfigsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult);

            var tags = await this.buildCannotGetDeliveryChannelConfigsEventTags.ExecuteBuildTagsForEventAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutNoChannelsToSendOnEventAsync(
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            IReadOnlyDictionary<IDeliveryChannel, PrepareToSendOnChannelResult> channelToPrepareToSendOnChannelResultMap,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new NoChannelsToSendOnEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, channelToPrepareToSendOnChannelResultMap, this.cannotStageToSendOnChannelAction);

            var tags = await this.buildNoChannelsToSendOnEventTags.ExecuteBuildTagsForEventAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutStagedToSendOnChannelsEventAsync(
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            IReadOnlyDictionary<IDeliveryChannel, PrepareToSendOnChannelResult> channelToPrepareToSendOnChannelResultMap,
            IReadOnlyCollection<IDeliveryChannel> channelsToSendOn,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new StagedToSendOnChannelsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, channelToPrepareToSendOnChannelResultMap, this.cannotStageToSendOnChannelAction, channelsToSendOn);

            var tags = await this.buildStagedToSendOnChannelEventTags.ExecuteBuildTagsForEventAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutSagaAsync(
            long trackingCodeId,
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationInstruction>> channelToOperationInstructionsMap,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var channelToOperationOutcomeSpecsMap = channelToOperationInstructionsMap.ToDictionary(
                _ => _.Key,
                _ => (IReadOnlyList<OperationOutcomeSpec>)_.Value.Select(c => new OperationOutcomeSpec(c.TrackingCodeId, c.SucceededEventType, c.FailedEventType)).ToList());

            var processSendNotificationSagaOp = new ProcessSendNotificationSagaOp(trackingCodeId, channelToOperationOutcomeSpecsMap);

            var @event = new ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>(trackingCodeId, processSendNotificationSagaOp, DateTime.UtcNow);

            var tags = await this.buildExecuteProcessSendNotificationSagaEventTags.ExecuteBuildTagsForEventAsync(trackingCodeId, @event, inheritableTags);

            await this.sagaStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutChannelOperationsAsync(
            IDeliveryChannel channel,
            IReadOnlyList<ChannelOperationInstruction> channelOperationInstructions)
        {
            var channelOperationStream = this.channelToOperationStreamMap[channel];

            foreach (var channelOperationInstruction in channelOperationInstructions)
            {
                var operation = channelOperationInstruction.Operation;

                // we need to use reflection here because streams do not yet support co- and contra-variance
                // ExecuteOpRequestedEvent<long, IOperation> cannot be handled by the client operation handlers.
                var executeOpRequestedEventType = typeof(ExecuteOpRequestedEvent<,>).MakeGenericType(typeof(long), operation.GetType());

                var executeOpRequestedEvent = executeOpRequestedEventType.Construct(channelOperationInstruction.TrackingCodeId, operation, DateTime.UtcNow, null);

                await channelOperationStream.PutWithIdAsync(channelOperationInstruction.TrackingCodeId, executeOpRequestedEvent, channelOperationInstruction.Tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
            }
        }

        private async Task<GetAudienceResult> GetAudienceAsync(
            INotification notification)
        {
            GetAudienceResult result;

            try
            {
                var getAudienceForNotificationOp = new GetAudienceOp(notification);

                result = await this.getAudienceProtocol.ExecuteAsync(getAudienceForNotificationOp);

                result.AsOp().Must().NotBeNull(Invariant($"Executing {nameof(GetAudienceOp)} should not return null."));
            }
            catch (Exception ex)
            {
                result = new GetAudienceResult(null, new[] { new ExceptionThrownFailure(ex.ToString()) }, FailureAction.Stop);
            }

            return result;
        }

        private async Task<GetDeliveryChannelConfigsResult> GetDeliveryChannelConfigsAsync(
            INotification notification,
            IAudience audience)
        {
            GetDeliveryChannelConfigsResult result;

            try
            {
                var getDeliveryChannelConfigsOp = new GetDeliveryChannelConfigsOp(notification, audience);

                result = await this.getDeliveryChannelConfigsProtocol.ExecuteAsync(getDeliveryChannelConfigsOp);

                result.AsOp().Must().NotBeNull(Invariant($"Executing {nameof(GetDeliveryChannelConfigsOp)} should not return null."));
            }
            catch (Exception ex)
            {
                result = new GetDeliveryChannelConfigsResult(null, new[] { new ExceptionThrownFailure(ex.ToString()) }, FailureAction.Stop);
            }

            return result;
        }

        private async Task<PrepareToSendOnChannelResult> PrepareToSendOnChannelAsync(
            INotification notification,
            IAudience audience,
            IDeliveryChannel channel,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            PrepareToSendOnChannelResult result;

            try
            {
                var prepareToSendOnChannelOp = new PrepareToSendOnChannelOp(notification, audience, channel, inheritableTags);

                result = await this.prepareToSendOnChannelProtocol.ExecuteAsync(prepareToSendOnChannelOp);

                result.AsOp().Must().NotBeNull(Invariant($"Executing {nameof(PrepareToSendOnChannelOp)} should not return null."));
            }
            catch (Exception ex)
            {
                result = new PrepareToSendOnChannelResult(null, new[] { new ExceptionThrownFailure(ex.ToString()) }, PrepareToSendOnChannelFailureAction.DoNotStageForSendingOnChannel);
            }

            return result;
        }
    }
}