// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleSendEmailProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Protocol.Email.Bot
{
    using System;
    using System.Threading.Tasks;
    using Naos.Database.Domain;
    using Naos.Email.Domain;
    using Naos.Notification.Domain;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Executes a <see cref="SendEmailOp"/>.
    /// </summary>
    public class HandleSendEmailProtocol : HandleRecordAsyncSpecificProtocolBase<ExecuteOpRequestedEvent<long, SendEmailOp>>
    {
        private readonly ISendEmailProtocol sendEmailProtocol;

        private readonly IWriteOnlyStream emailEventStream;

        private readonly IBuildTagsProtocol<SendEmailRequestedEvent<long>> buildSendEmailRequestedEventTagsProtocol;

        private readonly IBuildTagsProtocol<SendEmailResponseEventBase<long>> buildSendEmailResponseEventTagsProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleSendEmailProtocol"/> class.
        /// </summary>
        /// <param name="sendEmailProtocol">Protocol to send an email.</param>
        /// <param name="emailEventStream">The email event stream.</param>
        /// <param name="buildSendEmailRequestedEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendEmailRequestedEvent{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        /// <param name="buildSendEmailResponseEventTagsProtocol">OPTIONAL protocol to builds the tags to use when putting the <see cref="SendEmailResponseEventBase{TId}"/> into the Email Event Stream.  DEFAULT is to not add any tags; tags will be null.  Consider using <see cref="UseInheritableTagsProtocol{TEvent}"/> to just use the inheritable tags.</param>
        public HandleSendEmailProtocol(
            ISendEmailProtocol sendEmailProtocol,
            IWriteOnlyStream emailEventStream,
            IBuildTagsProtocol<SendEmailRequestedEvent<long>> buildSendEmailRequestedEventTagsProtocol = null,
            IBuildTagsProtocol<SendEmailResponseEventBase<long>> buildSendEmailResponseEventTagsProtocol = null)
        {
            new { sendEmailProtocol }.AsArg().Must().NotBeNull();
            new { emailEventStream }.AsArg().Must().NotBeNull();

            this.sendEmailProtocol = sendEmailProtocol;
            this.emailEventStream = emailEventStream;
            this.buildSendEmailRequestedEventTagsProtocol = buildSendEmailRequestedEventTagsProtocol;
            this.buildSendEmailResponseEventTagsProtocol = buildSendEmailResponseEventTagsProtocol;
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(
            HandleRecordOp<ExecuteOpRequestedEvent<long, SendEmailOp>> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            // Pull some info out of the operation.
            var emailTrackingCodeId = operation.RecordToHandle.Payload.Id;

            var sendEmailOp = operation.RecordToHandle.Payload.Operation;

            var inheritableTags = operation.RecordToHandle.Metadata.Tags;

            // Write the request to the event stream
            var sendEmailRequestedEvent = new SendEmailRequestedEvent<long>(emailTrackingCodeId, DateTime.UtcNow, sendEmailOp.SendEmailRequest);

            var tags = this.buildSendEmailRequestedEventTagsProtocol.ExecuteBuildTags(emailTrackingCodeId, sendEmailRequestedEvent, inheritableTags);

            await this.emailEventStream.PutWithIdAsync(emailTrackingCodeId, sendEmailRequestedEvent, tags, ExistingRecordStrategy.DoNotWriteIfFoundById);

            // Execute the operation
            var emailResponse = await this.sendEmailProtocol.ExecuteAsync(sendEmailOp);

            // Put response to event stream based on success/failure
            SendEmailResponseEventBase<long> emailResponseEventBase;

            if (emailResponse.SendEmailResult == SendEmailResult.Success)
            {
                emailResponseEventBase = new SucceededInSendingEmailEvent<long>(emailTrackingCodeId, DateTime.UtcNow, emailResponse);
            }
            else
            {
                emailResponseEventBase = new FailedToSendEmailEvent<long>(emailTrackingCodeId, DateTime.UtcNow, emailResponse);
            }

            tags = this.buildSendEmailResponseEventTagsProtocol.ExecuteBuildTags(emailTrackingCodeId, emailResponseEventBase, inheritableTags);

            await this.emailEventStream.PutWithIdAsync(emailTrackingCodeId, emailResponseEventBase, tags, ExistingRecordStrategy.DoNotWriteIfFoundByIdAndType);
        }
    }
}