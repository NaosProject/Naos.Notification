// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttemptToSendNotificationBaseEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The system attempted to send a notification on all channels that were prepared to send on.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class AttemptToSendNotificationBaseEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttemptToSendNotificationBaseEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="attemptToSendNotificationResult">The result of attempting to send the notification.</param>
        protected AttemptToSendNotificationBaseEvent(
            long id,
            DateTime timestampUtc,
            AttemptToSendNotificationResult attemptToSendNotificationResult)
            : base(id, timestampUtc)
        {
            new { attemptToSendNotificationResult }.AsArg().Must().NotBeNull();

            this.AttemptToSendNotificationResult = attemptToSendNotificationResult;
        }

        /// <summary>
        /// Gets the result of attempting to send the notification.
        /// </summary>
        public AttemptToSendNotificationResult AttemptToSendNotificationResult { get; private set; }
    }
}
