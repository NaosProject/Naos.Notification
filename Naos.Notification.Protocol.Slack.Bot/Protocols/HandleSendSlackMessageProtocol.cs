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
    using Naos.Slack.Domain;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Executes a <see cref="SendSlackMessageOp"/>.
    /// </summary>
    public class HandleSendSlackMessageProtocol : HandleRecordAsyncSpecificProtocolBase<ExecuteOpRequestedEvent<long, SendSlackMessageOp>>
    {
        private readonly ISendSlackMessageProtocol sendSlackMessageProtocol;

        private readonly IWriteOnlyStream slackEventStream;

        private readonly IBuildTagsProtocol<SendSlackMessageRequestedEvent<long>> buildSendSlackMessageRequestedEventTagsProtocol;

        private readonly IBuildTagsProtocol<SendSlackMessageResponseEventBase<long>> buildSendSlackMessageResponseEventTagsProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleSendSlackMessageProtocol"/> class.
        /// </summary>
        /// <param name="sendSlackMessageProtocol">Protocol to send a slack message.</param>
        /// <param name="slackEventStream">The Slack event stream.</param>
        /// <param name="buildSendSlackMessageRequestedEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendSlackMessageRequestedEvent{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildSendSlackMessageResponseEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendSlackMessageResponseEventBase{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        public HandleSendSlackMessageProtocol(
            ISendSlackMessageProtocol sendSlackMessageProtocol,
            IWriteOnlyStream slackEventStream,
            IBuildTagsProtocol<SendSlackMessageRequestedEvent<long>> buildSendSlackMessageRequestedEventTagsProtocol = null,
            IBuildTagsProtocol<SendSlackMessageResponseEventBase<long>> buildSendSlackMessageResponseEventTagsProtocol = null)
        {
            new { sendSlackMessageProtocol }.AsArg().Must().NotBeNull();
            new { slackEventStream }.AsArg().Must().NotBeNull();

            this.sendSlackMessageProtocol = sendSlackMessageProtocol;
            this.slackEventStream = slackEventStream;
            this.buildSendSlackMessageRequestedEventTagsProtocol = buildSendSlackMessageRequestedEventTagsProtocol;
            this.buildSendSlackMessageResponseEventTagsProtocol = buildSendSlackMessageResponseEventTagsProtocol;
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

            var tags = this.buildSendSlackMessageRequestedEventTagsProtocol.ExecuteBuildTags(slackOperationTrackingCodeId, sendSlackMessageRequestedEvent, inheritableTags);

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

            tags = this.buildSendSlackMessageResponseEventTagsProtocol.ExecuteBuildTags(slackOperationTrackingCodeId, sendSlackMessageResponseEvent, inheritableTags);

            await this.slackEventStream.PutWithIdAsync(slackOperationTrackingCodeId, sendSlackMessageResponseEvent, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundByIdAndType);
        }
    }
}