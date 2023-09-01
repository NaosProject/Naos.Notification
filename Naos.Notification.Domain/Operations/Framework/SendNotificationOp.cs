// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SendNotificationOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Sends a notification.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SendNotificationOp : ReturningOperationBase<NotificationTrackingCode>, IHaveTags, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendNotificationOp"/> class.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        /// <param name="tags">OPTIONAL tags to persist with the notification.  DEFAULT is none specified.</param>
        public SendNotificationOp(
            INotification notification,
            IReadOnlyCollection<NamedValue<string>> tags = null)
        {
            new { notification }.AsArg().Must().NotBeNull();
            new { tags }.AsArg().Must().NotContainAnyNullElementsWhenNotNull();

            this.Notification = notification;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the notification to send.
        /// </summary>
        public INotification Notification { get; private set; }

        /// <inheritdoc />
        public IReadOnlyCollection<NamedValue<string>> Tags { get; private set; }
    }
}
