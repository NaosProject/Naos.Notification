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

        private readonly IGetAudienceProtocol getAudienceProtocol;

        private readonly IGetDeliveryChannelConfigsProtocol getDeliveryChannelConfigsProtocol;

        private readonly IPrepareToSendOnChannelProtocol prepareToSendOnChannelProtocol;

        private readonly CannotPrepareToSendOnChannelAction cannotPrepareToSendOnChannelAction;

        private readonly IReadOnlyDictionary<IDeliveryChannel, IWriteOnlyStream> channelToOperationStreamMap;

        private readonly IBuildTagsProtocol<SendNotificationRequestedEvent> buildSendNotificationRequestedEventTagsProtocol;

        private readonly IBuildTagsProtocol<CouldNotGetOrUseAudienceEvent> buildCouldNotGetOrUseAudienceEventTagsProtocol;

        private readonly IBuildTagsProtocol<CouldNotGetOrUseDeliveryChannelConfigsEvent> buildCouldNotGetOrUseDeliveryChannelConfigsEventTagsProtocol;

        private readonly IBuildTagsProtocol<PrepareToSendNotificationEventBase> buildPrepareToSendNotificationEventTagsProtocol;

        private readonly IBuildTagsProtocol<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>> buildExecuteProcessSendNotificationSagaEventTagsProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleSendNotificationProtocol"/> class.
        /// </summary>
        /// <param name="eventStream">The Notification Event Stream.</param>
        /// <param name="sagaStream">The Notification Saga Stream.</param>
        /// <param name="getAudienceProtocol">Executes a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsProtocol">Executes a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <param name="prepareToSendOnChannelProtocol">Executes a <see cref="PrepareToSendOnChannelOp"/>.</param>
        /// <param name="cannotPrepareToSendOnChannelAction">Specifies what to do when we encounter a situation where we cannot prepare to send on a channel.</param>
        /// <param name="channelToOperationStreamMap">A map of delivery channel to the channel's operation stream.</param>
        /// <param name="buildSendNotificationRequestedEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendNotificationRequestedEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildCouldNotGetOrUseAudienceEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="CouldNotGetOrUseAudienceEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildCouldNotGetOrUseDeliveryChannelConfigsEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="CouldNotGetOrUseDeliveryChannelConfigsEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildPrepareToSendNotificationEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="PrepareToSendNotificationEventBase"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildExecuteProcessSendNotificationSagaEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="ProcessSendNotificationSagaOp"/> into the Notification Saga Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        protected HandleSendNotificationProtocol(
            IWriteOnlyStream eventStream,
            IWriteOnlyStream sagaStream,
            IGetAudienceProtocol getAudienceProtocol,
            IGetDeliveryChannelConfigsProtocol getDeliveryChannelConfigsProtocol,
            IPrepareToSendOnChannelProtocol prepareToSendOnChannelProtocol,
            CannotPrepareToSendOnChannelAction cannotPrepareToSendOnChannelAction,
            IReadOnlyDictionary<IDeliveryChannel, IWriteOnlyStream> channelToOperationStreamMap,
            IBuildTagsProtocol<SendNotificationRequestedEvent> buildSendNotificationRequestedEventTagsProtocol = null,
            IBuildTagsProtocol<CouldNotGetOrUseAudienceEvent> buildCouldNotGetOrUseAudienceEventTagsProtocol = null,
            IBuildTagsProtocol<CouldNotGetOrUseDeliveryChannelConfigsEvent> buildCouldNotGetOrUseDeliveryChannelConfigsEventTagsProtocol = null,
            IBuildTagsProtocol<PrepareToSendNotificationEventBase> buildPrepareToSendNotificationEventTagsProtocol = null,
            IBuildTagsProtocol<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>> buildExecuteProcessSendNotificationSagaEventTagsProtocol = null)
        {
            new { eventStream }.AsArg().Must().NotBeNull();
            new { sagaStream }.AsArg().Must().NotBeNull();
            new { getAudienceProtocol }.AsArg().Must().NotBeNull();
            new { getDeliveryChannelConfigsProtocol }.AsArg().Must().NotBeNull();
            new { prepareToSendOnChannelProtocol }.AsArg().Must().NotBeNull();
            new { cannotPrepareToSendOnChannelAction }.AsArg().Must().NotBeEqualTo(CannotPrepareToSendOnChannelAction.Unknown);
            new { channelToOperationStreamMap }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();

            this.eventStream = eventStream;
            this.sagaStream = sagaStream;
            this.getAudienceProtocol = getAudienceProtocol;
            this.getDeliveryChannelConfigsProtocol = getDeliveryChannelConfigsProtocol;
            this.prepareToSendOnChannelProtocol = prepareToSendOnChannelProtocol;
            this.cannotPrepareToSendOnChannelAction = cannotPrepareToSendOnChannelAction;
            this.channelToOperationStreamMap = channelToOperationStreamMap;
            this.buildSendNotificationRequestedEventTagsProtocol = buildSendNotificationRequestedEventTagsProtocol;
            this.buildCouldNotGetOrUseAudienceEventTagsProtocol = buildCouldNotGetOrUseAudienceEventTagsProtocol;
            this.buildCouldNotGetOrUseDeliveryChannelConfigsEventTagsProtocol = buildCouldNotGetOrUseDeliveryChannelConfigsEventTagsProtocol;
            this.buildPrepareToSendNotificationEventTagsProtocol = buildPrepareToSendNotificationEventTagsProtocol;
            this.buildExecuteProcessSendNotificationSagaEventTagsProtocol = buildExecuteProcessSendNotificationSagaEventTagsProtocol;
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

            // Get the audience and write CannotGetOrUseAudienceEvent
            // to the Event Stream if there is none of if we should stop on failures.
            var getAudienceResult = await this.GetAudienceAsync(notification);

            var getAudienceOutcome = getAudienceResult.GetOutcome();

            if ((getAudienceOutcome != GetAudienceOutcome.GotAudienceWithNoFailuresReported) &&
                (getAudienceOutcome != GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored))
            {
                await this.PutCannotGetOrUseAudienceEventAsync(getAudienceResult, trackingCodeId, tags);

                return;
            }

            // Get the delivery channel configs and write CannotGetOrUseDeliveryChannelConfigsEvent
            // to the Event Stream if there are none or if we should stop on failures.
            var audience = getAudienceResult.Audience;

            var getDeliveryChannelConfigsResult = await this.GetDeliveryChannelConfigsAsync(notification, getAudienceResult.Audience);

            var getDeliveryChannelConfigsOutcome = getDeliveryChannelConfigsResult.GetOutcome();

            if ((getDeliveryChannelConfigsOutcome != GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithNoFailuresReported) &&
                (getDeliveryChannelConfigsOutcome != GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithReportedFailuresIgnored))
            {
                await this.PutCannotGetOrUseDeliveryChannelConfigsEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, trackingCodeId, tags);

                return;
            }

            // Prepare the notification to be sent to the audience on all channels configured for sending.
            var channelConfigs = getDeliveryChannelConfigsResult.Configs;

            var channelToPrepareToSendOnChannelResultMap = new Dictionary<IDeliveryChannel, PrepareToSendOnChannelResult>();

            var channelToOperationInstructionsMap = new Dictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationInstruction>>();

            foreach (var channelConfig in channelConfigs)
            {
                if (channelConfig.Action == DeliveryChannelAction.AudienceOptedOut)
                {
                    continue;
                }
                else if (channelConfig.Action == DeliveryChannelAction.SendOnChannel)
                {
                    // no-op
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(DeliveryChannelAction)} is not supported: {channelConfig.Action}."));
                }

                var channel = channelConfig.Channel;

                var prepareToSendOnChannelResult = await this.PrepareToSendOnChannelAsync(notification, audience, channel, tags);

                var prepareToSendOnChannelOutcome = prepareToSendOnChannelResult.GetOutcome();

                channelToPrepareToSendOnChannelResultMap.Add(channel, prepareToSendOnChannelResult);

                if ((prepareToSendOnChannelOutcome == PrepareToSendOnChannelOutcome.PreparedToSendOnChannelWithNoFailuresReported) ||
                    (prepareToSendOnChannelOutcome == PrepareToSendOnChannelOutcome.PreparedToSendOnChannelWithReportedFailuresIgnored))
                {
                    new { this.channelToOperationStreamMap }.AsOp().Must().ContainKey(channel, Invariant($"Staging to send on channel {channel.GetType().ToStringReadable()} but there is no operation stream associated with that channel."));

                    channelToOperationInstructionsMap.Add(channel, prepareToSendOnChannelResult.ChannelOperationInstructions);
                }
                else
                {
                    if (this.cannotPrepareToSendOnChannelAction == CannotPrepareToSendOnChannelAction.ContinueAndAttemptPreparingToSendOnNextChannel)
                    {
                        // no-op
                    }
                    else if (this.cannotPrepareToSendOnChannelAction == CannotPrepareToSendOnChannelAction.StopAndNotDoNotSendOnAnyChannel)
                    {
                        // discard all channel operations
                        channelToOperationInstructionsMap = new Dictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationInstruction>>();

                        break;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(CannotPrepareToSendOnChannelAction)} is not supported: {this.cannotPrepareToSendOnChannelAction}."));
                    }
                }
            }

            var prepareToSendNotificationResult = new PrepareToSendNotificationResult(channelToPrepareToSendOnChannelResultMap, this.cannotPrepareToSendOnChannelAction, channelToOperationInstructionsMap.Keys.ToList());

            var prepareToSendNotificationOutcome = prepareToSendNotificationResult.GetOutcome();

            await this.PutPrepareToSendNotificationEventAsync(prepareToSendNotificationOutcome, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendNotificationResult, trackingCodeId, tags);

            // Is at least one channel is prepared for sending?  Continue.
            if ((prepareToSendNotificationOutcome != PrepareToSendNotificationOutcome.PreparedToSendOnAllChannels) &&
                (prepareToSendNotificationOutcome != PrepareToSendNotificationOutcome.PreparedToSendOnSomeChannels))
            {
                return;
            }

            // Create a Saga to track channel operations.
            await this.PutSagaAsync(trackingCodeId, channelToOperationInstructionsMap, tags);

            // Push the channel-operations to their streams.
            foreach (var channel in channelToOperationInstructionsMap.Keys)
            {
                var channelOperationInstructions = channelToOperationInstructionsMap[channel];

                await this.PutChannelOperationsAsync(channel, channelOperationInstructions);
            }
        }

        private async Task PutSendNotificationRequestedEventAsync(
            SendNotificationOp sendNotificationOp,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new SendNotificationRequestedEvent(trackingCodeId, DateTime.UtcNow, sendNotificationOp);

            var tags = await this.buildSendNotificationRequestedEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutCannotGetOrUseAudienceEventAsync(
            GetAudienceResult getAudienceResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new CouldNotGetOrUseAudienceEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult);

            var tags = await this.buildCouldNotGetOrUseAudienceEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutCannotGetOrUseDeliveryChannelConfigsEventAsync(
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new CouldNotGetOrUseDeliveryChannelConfigsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult);

            var tags = await this.buildCouldNotGetOrUseDeliveryChannelConfigsEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutPrepareToSendNotificationEventAsync(
            PrepareToSendNotificationOutcome prepareToSendNotificationOutcome,
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            PrepareToSendNotificationResult prepareToSendNotificationResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            PrepareToSendNotificationEventBase @event;

            switch (prepareToSendNotificationOutcome)
            {
                case PrepareToSendNotificationOutcome.AudienceOptedOutOfAllChannels:
                    @event = new AudienceOptedOutOfAllChannelsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendNotificationResult);
                    break;
                case PrepareToSendNotificationOutcome.PreparedToSendOnAllChannels:
                    @event = new PreparedToSendOnAllChannelsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendNotificationResult);
                    break;
                case PrepareToSendNotificationOutcome.PreparedToSendOnSomeChannels:
                    @event = new PreparedToSendOnSomeChannelsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendNotificationResult);
                    break;
                case PrepareToSendNotificationOutcome.CouldNotPrepareToSendOnAnyChannelDespiteAttemptingAll:
                case PrepareToSendNotificationOutcome.CouldNotPrepareToSendOnAnyChannelBecauseOneForcedAllToBeDiscarded:
                    @event = new CouldNotPrepareToSendOnAnyChannelEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendNotificationResult);
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(PrepareToSendNotificationOutcome)} is not supported: {prepareToSendNotificationOutcome}"));
            }

            var tags = await this.buildPrepareToSendNotificationEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutSagaAsync(
            long trackingCodeId,
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationInstruction>> channelToOperationInstructionsMap,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var channelToOperationsMonitoringInfoMap = channelToOperationInstructionsMap.ToDictionary(
                _ => _.Key,
                _ => (IReadOnlyList<ChannelOperationMonitoringInfo>)_.Value.Select(c => c.MonitoringInfo).ToList());

            var processSendNotificationSagaOp = new ProcessSendNotificationSagaOp(trackingCodeId, channelToOperationsMonitoringInfoMap);

            var @event = new ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>(trackingCodeId, processSendNotificationSagaOp, DateTime.UtcNow);

            var tags = await this.buildExecuteProcessSendNotificationSagaEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

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

                var channelOperationTrackingCodeId = channelOperationInstruction.MonitoringInfo.ChannelOperationTrackingCodeId;

                var executeOpRequestedEvent = executeOpRequestedEventType.Construct(channelOperationTrackingCodeId, operation, DateTime.UtcNow, null);

                await channelOperationStream.PutWithIdAsync(channelOperationTrackingCodeId, executeOpRequestedEvent, channelOperationInstruction.Tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
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
                result = new PrepareToSendOnChannelResult(null, new[] { new ExceptionThrownFailure(ex.ToString()) }, PrepareToSendOnChannelFailureAction.DoNotSendOnChannel);
            }

            return result;
        }
    }
}