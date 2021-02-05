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

        private readonly IBuildTagsProtocol<CannotGetAudienceEvent> buildCannotGetAudienceEventTagsProtocol;

        private readonly IBuildTagsProtocol<CannotGetDeliveryChannelConfigsEvent> buildCannotGetDeliveryChannelConfigsEventTagsProtocol;

        private readonly IBuildTagsProtocol<NoChannelsToSendOnEvent> buildNoChannelsToSendOnEventTagsProtocol;

        private readonly IBuildTagsProtocol<PreparedToSendOnChannelsEvent> buildPreparedToSendOnChannelEventTagsProtocol;

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
        /// <param name="buildCannotGetAudienceEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="CannotGetAudienceEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildCannotGetDeliveryChannelConfigsEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="CannotGetDeliveryChannelConfigsEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildNoChannelsToSendOnEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="NoChannelsToSendOnEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildPreparedToSendOnChannelEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="PreparedToSendOnChannelsEvent"/> into the Notification Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
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
            IBuildTagsProtocol<CannotGetAudienceEvent> buildCannotGetAudienceEventTagsProtocol = null,
            IBuildTagsProtocol<CannotGetDeliveryChannelConfigsEvent> buildCannotGetDeliveryChannelConfigsEventTagsProtocol = null,
            IBuildTagsProtocol<NoChannelsToSendOnEvent> buildNoChannelsToSendOnEventTagsProtocol = null,
            IBuildTagsProtocol<PreparedToSendOnChannelsEvent> buildPreparedToSendOnChannelEventTagsProtocol = null,
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
            this.buildCannotGetAudienceEventTagsProtocol = buildCannotGetAudienceEventTagsProtocol;
            this.buildCannotGetDeliveryChannelConfigsEventTagsProtocol = buildCannotGetDeliveryChannelConfigsEventTagsProtocol;
            this.buildNoChannelsToSendOnEventTagsProtocol = buildNoChannelsToSendOnEventTagsProtocol;
            this.buildPreparedToSendOnChannelEventTagsProtocol = buildPreparedToSendOnChannelEventTagsProtocol;
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

            // Get the audience and write PutNoAudienceEventAsync
            // to the Event Stream if there is none of if we should stop on failures.
            var getAudienceResult = await this.GetAudienceAsync(notification);

            var getAudienceOutcome = getAudienceResult.GetOutcome();

            if ((getAudienceOutcome != GetAudienceOutcome.GotAudienceWithNoFailuresReported) &&
                (getAudienceOutcome != GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored))
            {
                await this.PutCannotGetAudienceEventAsync(getAudienceResult, trackingCodeId, tags);

                return;
            }

            // Get the delivery channel configs and write CannotGetDeliveryChannelConfigsEvent
            // to the Event Stream if there are none or if we should stop on failures.
            var audience = getAudienceResult.Audience;

            var getDeliveryChannelConfigsResult = await this.GetDeliveryChannelConfigsAsync(notification, getAudienceResult.Audience);

            var getDeliveryChannelConfigsOutcome = getDeliveryChannelConfigsResult.GetOutcome();

            if ((getDeliveryChannelConfigsOutcome != GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithNoFailuresReported) &&
                (getDeliveryChannelConfigsOutcome != GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithReportedFailuresIgnored))
            {
                await this.PutCannotGetDeliveryChannelConfigsEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, trackingCodeId, tags);

                return;
            }

            // Prepare to send on each channel.
            // Write NoChannelsToSendOnEventAsync if none of the channels can be prepared.
            var channelConfigs = getDeliveryChannelConfigsResult.Configs;

            var channelToPrepareToSendOnChannelResultMap = new Dictionary<IDeliveryChannel, PrepareToSendOnChannelResult>();

            var channelToOperationInstructionsMap = new Dictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationInstruction>>();

            PrepareToSendOnAllChannelsResult prepareToSendOnAllChannelsResult;

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
                        prepareToSendOnAllChannelsResult = new PrepareToSendOnAllChannelsResult(channelToPrepareToSendOnChannelResultMap, this.cannotPrepareToSendOnChannelAction, new IDeliveryChannel[0]);

                        await this.PutNoChannelsToSendOnEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendOnAllChannelsResult, trackingCodeId, tags);

                        return;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(CannotPrepareToSendOnChannelAction)} is not supported: {this.cannotPrepareToSendOnChannelAction}."));
                    }
                }
            }

            prepareToSendOnAllChannelsResult = new PrepareToSendOnAllChannelsResult(channelToPrepareToSendOnChannelResultMap, this.cannotPrepareToSendOnChannelAction, channelToOperationInstructionsMap.Keys.ToList());

            if (!channelToOperationInstructionsMap.Any())
            {
                await this.PutNoChannelsToSendOnEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendOnAllChannelsResult, trackingCodeId, tags);

                return;
            }

            // Channels are prepared for sending, write PreparedToSendOnChannelsEventAsync.
            await this.PutPreparedToSendOnChannelsEventAsync(getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendOnAllChannelsResult, trackingCodeId, tags);

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

        private async Task PutCannotGetAudienceEventAsync(
            GetAudienceResult getAudienceResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new CannotGetAudienceEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult);

            var tags = await this.buildCannotGetAudienceEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutCannotGetDeliveryChannelConfigsEventAsync(
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new CannotGetDeliveryChannelConfigsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult);

            var tags = await this.buildCannotGetDeliveryChannelConfigsEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutNoChannelsToSendOnEventAsync(
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            PrepareToSendOnAllChannelsResult prepareToSendOnAllChannelsResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new NoChannelsToSendOnEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendOnAllChannelsResult);

            var tags = await this.buildNoChannelsToSendOnEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutPreparedToSendOnChannelsEventAsync(
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            PrepareToSendOnAllChannelsResult prepareToSendOnAllChannelsResult,
            long trackingCodeId,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var @event = new PreparedToSendOnChannelsEvent(trackingCodeId, DateTime.UtcNow, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendOnAllChannelsResult);

            var tags = await this.buildPreparedToSendOnChannelEventTagsProtocol.ExecuteBuildTagsAsync(trackingCodeId, @event, inheritableTags);

            await this.eventStream.PutWithIdAsync(trackingCodeId, @event, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
        }

        private async Task PutSagaAsync(
            long trackingCodeId,
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationInstruction>> channelToOperationInstructionsMap,
            IReadOnlyDictionary<string, string> inheritableTags)
        {
            var channelToOperationOutcomeSpecsMap = channelToOperationInstructionsMap.ToDictionary(
                _ => _.Key,
                _ => (IReadOnlyList<ChannelOperationOutcomeSpec>)_.Value.Select(c => c.OutcomeSpec).ToList());

            var processSendNotificationSagaOp = new ProcessSendNotificationSagaOp(trackingCodeId, channelToOperationOutcomeSpecsMap);

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

                var channelTrackingCodeId = channelOperationInstruction.OutcomeSpec.ChannelTrackingCodeId;

                var executeOpRequestedEvent = executeOpRequestedEventType.Construct(channelTrackingCodeId, operation, DateTime.UtcNow, null);

                await channelOperationStream.PutWithIdAsync(channelTrackingCodeId, executeOpRequestedEvent, channelOperationInstruction.Tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);
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