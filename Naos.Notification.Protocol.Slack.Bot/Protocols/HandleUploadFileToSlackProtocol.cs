// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleUploadFileToSlackProtocol.cs" company="Naos Project">
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
    /// Executes a <see cref="UploadFileToSlackOp"/>.
    /// </summary>
    public class HandleUploadFileToSlackProtocol : HandleRecordAsyncSpecificProtocolBase<ExecuteOpRequestedEvent<long, UploadFileToSlackOp>>
    {
        private readonly IUploadFileToSlackProtocol uploadFileToSlackProtocol;

        private readonly IWriteOnlyStream slackEventStream;

        private readonly IBuildTagsForEvent<UploadFileToSlackRequestedEvent<long>> buildUploadFileToSlackRequestedEventTags;

        private readonly IBuildTagsForEvent<UploadFileToSlackResponseEventBase<long>> buildUploadFileToSlackResponseEventTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleUploadFileToSlackProtocol"/> class.
        /// </summary>
        /// <param name="uploadFileToSlackProtocol">Protocol to upload a file to Slack.</param>
        /// <param name="slackEventStream">The Slack event stream.</param>
        /// <param name="buildUploadFileToSlackRequestedEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="UploadFileToSlackRequestedEvent{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildUploadFileToSlackResponseEventTags">OPTIONAL protocol to builds the tags to use when putting the <see cref="UploadFileToSlackResponseEventBase{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsForEventProtocol{TEvent}"/> to just use the inheritable tags.</param>
        public HandleUploadFileToSlackProtocol(
            IUploadFileToSlackProtocol uploadFileToSlackProtocol,
            IWriteOnlyStream slackEventStream,
            IBuildTagsForEvent<UploadFileToSlackRequestedEvent<long>> buildUploadFileToSlackRequestedEventTags = null,
            IBuildTagsForEvent<UploadFileToSlackResponseEventBase<long>> buildUploadFileToSlackResponseEventTags = null)
        {
            new { uploadFileToSlackProtocol }.AsArg().Must().NotBeNull();
            new { slackEventStream }.AsArg().Must().NotBeNull();

            this.uploadFileToSlackProtocol = uploadFileToSlackProtocol;
            this.slackEventStream = slackEventStream;
            this.buildUploadFileToSlackRequestedEventTags = buildUploadFileToSlackRequestedEventTags;
            this.buildUploadFileToSlackResponseEventTags = buildUploadFileToSlackResponseEventTags;
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(
            HandleRecordOp<ExecuteOpRequestedEvent<long, UploadFileToSlackOp>> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            // Pull some info out of the operation.
            var slackOperationTrackingCodeId = operation.RecordToHandle.Payload.Id;

            var uploadFileToSlackOp = operation.RecordToHandle.Payload.Operation;

            var inheritableTags = operation.RecordToHandle.Metadata.Tags;

            // Write the request to the event stream
            var uploadFileToSlackRequestedEvent = new UploadFileToSlackRequestedEvent<long>(slackOperationTrackingCodeId, DateTime.UtcNow, uploadFileToSlackOp.UploadFileToSlackRequest);

            var tags = await this.buildUploadFileToSlackRequestedEventTags.ExecuteBuildTagsForEventAsync(slackOperationTrackingCodeId, uploadFileToSlackRequestedEvent, inheritableTags);

            await this.slackEventStream.PutWithIdAsync(slackOperationTrackingCodeId, uploadFileToSlackRequestedEvent, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);

            // Execute the operation
            var uploadFileToSlackResponse = await this.uploadFileToSlackProtocol.ExecuteAsync(uploadFileToSlackOp);

            // Put response to event stream based on success/failure
            UploadFileToSlackResponseEventBase<long> uploadFileToSlackResponseEvent;

            if (uploadFileToSlackResponse.UploadFileToSlackResult == UploadFileToSlackResult.Succeeded)
            {
                uploadFileToSlackResponseEvent = new SucceededInUploadingFileToSlackEvent<long>(slackOperationTrackingCodeId, DateTime.UtcNow, uploadFileToSlackResponse);
            }
            else
            {
                uploadFileToSlackResponseEvent = new FailedToUploadFileToSlackEvent<long>(slackOperationTrackingCodeId, DateTime.UtcNow, uploadFileToSlackResponse);
            }

            tags = await this.buildUploadFileToSlackResponseEventTags.ExecuteBuildTagsForEventAsync(slackOperationTrackingCodeId, uploadFileToSlackResponseEvent, inheritableTags);

            await this.slackEventStream.PutWithIdAsync(slackOperationTrackingCodeId, uploadFileToSlackResponseEvent, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundByIdAndType);
        }
    }
}