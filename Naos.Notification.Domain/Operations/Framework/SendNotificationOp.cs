// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SendNotificationOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Sends a notification.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SendNotificationOp : ReturningOperationBase<NotificationTrackingCode>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendNotificationOp"/> class.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        public SendNotificationOp(
            INotification notification)
        {
            new { notification }.AsArg().Must().NotBeNull();

            this.Notification = notification;
        }

        /// <summary>
        /// Gets the notification to send.
        /// </summary>
        public INotification Notification { get; private set; }
    }
}
