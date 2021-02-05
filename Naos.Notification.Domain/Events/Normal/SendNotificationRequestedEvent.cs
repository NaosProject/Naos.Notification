// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SendNotificationRequestedEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// A client has requested that the system send a notification.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SendNotificationRequestedEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendNotificationRequestedEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="sendNotificationOp">The operation to send a notification.</param>
        public SendNotificationRequestedEvent(
            long id,
            DateTime timestampUtc,
            SendNotificationOp sendNotificationOp)
            : base(id, timestampUtc)
        {
            new { sendNotificationOp }.AsArg().Must().NotBeNull();

            this.SendNotificationOp = sendNotificationOp;
        }

        /// <summary>
        /// Gets the operation to send a notification.
        /// </summary>
        public SendNotificationOp SendNotificationOp { get; private set; }
    }
}