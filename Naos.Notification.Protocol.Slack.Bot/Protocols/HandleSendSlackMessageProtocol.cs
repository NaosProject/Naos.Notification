// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleSendSlackMessageProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Protocol.Slack.Bot
{
    using System;
    using System.Threading.Tasks;

    using Naos.Database.Domain;
    using Naos.Notification.Domain;
    using Naos.Protocol.Domain;
    using Naos.Slack.Domain;

    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Executes a <see cref="SendSlackMessageOp"/>.
    /// </summary>
    public class HandleSendSlackMessageProtocol : HandleRecordAsyncSpecificProtocolBase<ExecuteOpRequestedEvent<long, SendSlackMessageOp>>
    {
        private readonly ISendSlackMessageProtocol sendSlackMessageProtocol;

        private readonly IWriteOnlyStream slackEventStream;

        private readonly IBuildTagsForEvent<SendSlackMessageRequestedEvent<long>> buildSendSlackMessageRequestedEventTags;

        private readonly IBuildTagsForEvent<SendSlackMessageResponseEventBase<long>> buildSendSlackMessageResponseEventTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleSendSlackMessageProtocol"/> class.
        /// </summary>
        /// <param name="sendSlackMessageProtocol">Protocol to send a slack message.</param>
        /// <param name="slackEventStream">The Slack event stream.</param>
        /// <param name="buildSendSlackMessageRequestedEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendSlackMessageRequestedEvent{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildSendSlackMessageResponseEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendSlackMessageResponseEventBase{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        public HandleSendSlackMessageProtocol(
            ISendSlackMessageProtocol sendSlackMessageProtocol,
            IWriteOnlyStream slackEventStream,
            IBuildTagsForEvent<SendSlackMessageRequestedEvent<long>> buildSendSlackMessageRequestedEventTags = null,
            IBuildTagsForEvent<SendSlackMessageResponseEventBase<long>> buildSendSlackMessageResponseEventTags = null)
        {
            new { sendSlackMessageProtocol }.AsArg().Must().NotBeNull();
            new { slackEventStream }.AsArg().Must().NotBeNull();

            this.sendSlackMessageProtocol = sendSlackMessageProtocol;
            this.slackEventStream = slackEventStream;
            this.buildSendSlackMessageRequestedEventTags = buildSendSlackMessageRequestedEventTags;
            this.buildSendSlackMessageResponseEventTags = buildSendSlackMessageResponseEventTags;
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(
            HandleRecordOp<ExecuteOpRequestedEvent<long, SendSlackMessageOp>> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            // Pull some info out of the operation.
            var slackOperationTrackingCodeId = operation.RecordToHandle.Payload.Id;

            var sendSlackMessageOp = operation.RecordToHandle.Payload.Operation;

            var inheritableTags = operation.RecordToHandle.Metadata.Tags;

            // Write the request to the event stream
            var sendSlackMessageRequestedEvent = new SendSlackMessageRequestedEvent<long>(slackOperationTrackingCodeId, DateTime.UtcNow, sendSlackMessageOp.SendSlackMessageRequest);

            var tags = await this.buildSendSlackMessageRequestedEventTags.ExecuteBuildTagsForEventAsync(slackOperationTrackingCodeId, sendSlackMessageRequestedEvent, inheritableTags);

            await this.slackEventStream.PutWithIdAsync(slackOperationTrackingCodeId, sendSlackMessageRequestedEvent, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);

            // Execute the operation
            var sendSlackMessageResponse = await this.sendSlackMessageProtocol.ExecuteAsync(sendSlackMessageOp);

            // Put response to event stream based on success/failure
            SendSlackMessageResponseEventBase<long> sendSlackMessageResponseEvent;

            if (sendSlackMessageResponse.SendSlackMessageResult == SendSlackMessageResult.Succeeded)
            {
                sendSlackMessageResponseEvent = new SucceededInSendingSlackMessageEvent<long>(slackOperationTrackingCodeId, DateTime.UtcNow, sendSlackMessageResponse);
            }
            else
            {
                sendSlackMessageResponseEvent = new FailedToSendSlackMessageEvent<long>(slackOperationTrackingCodeId, DateTime.UtcNow, sendSlackMessageResponse);
            }

            tags = await this.buildSendSlackMessageResponseEventTags.ExecuteBuildTagsForEventAsync(slackOperationTrackingCodeId, sendSlackMessageResponseEvent, inheritableTags);

            await this.slackEventStream.PutWithIdAsync(slackOperationTrackingCodeId, sendSlackMessageResponseEvent, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundByIdAndType);
        }
    }
}