﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SendNotificationProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Protocol.Client
{
    using System;
    using System.Threading.Tasks;

    using Naos.Database.Domain;
    using Naos.Notification.Domain;
    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Executes a <see cref="SendNotificationOp"/>.
    /// </summary>
    public class SendNotificationProtocol : AsyncSpecificReturningProtocolBase<SendNotificationOp, NotificationTrackingCode>, ISendNotificationProtocol
    {
        private readonly IWriteOnlyStream clientOperationStream;

        private readonly IBuildTagsProtocol<ExecuteOpRequestedEvent<long, SendNotificationOp>> buildExecuteSendNotificationEventTagsProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendNotificationProtocol"/> class.
        /// </summary>
        /// <param name="clientOperationStream">The client operation stream.</param>
        /// <param name="buildExecuteSendNotificationEventTagsProtocol">OPTIONAL protocol that executes a <see cref="BuildTagsOp{TEvent}"/>.  DEFAULT is null (no tags added when putting operation into stream).</param>
        protected SendNotificationProtocol(
            IWriteOnlyStream clientOperationStream,
            IBuildTagsProtocol<ExecuteOpRequestedEvent<long, SendNotificationOp>> buildExecuteSendNotificationEventTagsProtocol = null)
        {
            new { clientOperationStream }.AsArg().Must().NotBeNull();

            this.clientOperationStream = clientOperationStream;
            this.buildExecuteSendNotificationEventTagsProtocol = buildExecuteSendNotificationEventTagsProtocol;
        }

        /// <inheritdoc />
        public override async Task<NotificationTrackingCode> ExecuteAsync(
            SendNotificationOp operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            var notificationTrackingCodeId = await this.clientOperationStream.GetNextUniqueLongAsync();

            var result = new NotificationTrackingCode(notificationTrackingCodeId);

            var executeOperationRequestedEvent = new ExecuteOpRequestedEvent<long, SendNotificationOp>(notificationTrackingCodeId, operation, DateTime.UtcNow);

            var tags = await this.buildExecuteSendNotificationEventTagsProtocol.ExecuteBuildTagsAsync(result.Id, executeOperationRequestedEvent);

            await this.clientOperationStream.PutWithIdAsync(notificationTrackingCodeId, executeOperationRequestedEvent, tags, ExistingRecordEncounteredStrategy.DoNotWriteIfFoundById);

            return result;
        }
    }
}