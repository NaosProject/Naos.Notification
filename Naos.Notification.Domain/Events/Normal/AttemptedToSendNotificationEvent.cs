// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttemptedToSendNotificationEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The system attempted to send the notification on all channels that were staged to send on.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class AttemptedToSendNotificationEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttemptedToSendNotificationEvent"/> class.
        /// </summary>
        /// <remarks>
        /// The same communication channel may appear in both <paramref name="succeededChannels"/> and <paramref name="failedChannels"/>
        /// if there were multiple operations executed for that channel where some succeeded and some failed.
        /// </remarks>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="succeededChannels">The communication channels over which the notification was successfully sent.</param>
        /// <param name="failedChannels">The communication channels over which the notification failed to be sent.</param>
        public AttemptedToSendNotificationEvent(
            long id,
            DateTime timestampUtc,
            IReadOnlyCollection<IDeliveryChannel> succeededChannels,
            IReadOnlyCollection<IDeliveryChannel> failedChannels)
            : base(id, timestampUtc)
        {
            new { succeededChannels }.AsArg().Must().NotBeNull().And().ContainOnlyDistinctElements();
            new { failedChannels }.AsArg().Must().NotBeNull().And().ContainOnlyDistinctElements();

            this.SucceededChannels = succeededChannels;
            this.FailedChannels = failedChannels;
        }

        /// <summary>
        /// Gets the communication channels over which the notification was successfully sent.
        /// </summary>
        public IReadOnlyCollection<IDeliveryChannel> SucceededChannels { get; private set; }

        /// <summary>
        /// Gets the communication channels over which the notification failed to be sent.
        /// </summary>
        public IReadOnlyCollection<IDeliveryChannel> FailedChannels { get; private set; }
    }
}
