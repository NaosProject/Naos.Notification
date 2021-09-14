// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetDeliveryChannelConfigsOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the configured delivery channels for the specified notification and audience.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetDeliveryChannelConfigsOp : ReturningOperationBase<GetDeliveryChannelConfigsResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDeliveryChannelConfigsOp"/> class.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        /// <param name="audience">The audience for the notification.</param>
        public GetDeliveryChannelConfigsOp(
            INotification notification,
            IAudience audience)
        {
            new { notification }.AsArg().Must().NotBeNull();
            new { audience }.AsArg().Must().NotBeNull();

            this.Notification = notification;
            this.Audience = audience;
        }

        /// <summary>
        /// Gets the notification to send.
        /// </summary>
        public INotification Notification { get; private set; }

        /// <summary>
        /// Gets the audience for the notification.
        /// </summary>
        public IAudience Audience { get; private set; }
    }
}
