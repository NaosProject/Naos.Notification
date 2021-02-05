// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendOnChannelOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;

    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Prepares to send a notification to an audience on a channel.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PrepareToSendOnChannelOp : ReturningOperationBase<PrepareToSendOnChannelResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrepareToSendOnChannelOp"/> class.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        /// <param name="audience">The audience for the notification.</param>
        /// <param name="deliveryChannel">The channel on which to deliver the notification.</param>
        /// <param name="inheritableTags">OPTIONAL tags that can be inherited from a prior step in the workflow.  DEFAULT is null, indicating that there was no prior step or that no tags have been established in the workflow.</param>
        public PrepareToSendOnChannelOp(
            INotification notification,
            IAudience audience,
            IDeliveryChannel deliveryChannel,
            IReadOnlyDictionary<string, string> inheritableTags = null)
        {
            new { notification }.AsArg().Must().NotBeNull();
            new { audience }.AsArg().Must().NotBeNull();
            new { deliveryChannel }.AsArg().Must().NotBeNull();

            this.Notification = notification;
            this.Audience = audience;
            this.DeliveryChannel = deliveryChannel;
            this.InheritableTags = inheritableTags;
        }

        /// <summary>
        /// Gets the notification to send.
        /// </summary>
        public INotification Notification { get; private set; }

        /// <summary>
        /// Gets the audience for the notification.
        /// </summary>
        public IAudience Audience { get; private set; }

        /// <summary>
        /// Gets the channel on which to deliver the notification.
        /// </summary>
        public IDeliveryChannel DeliveryChannel { get; private set; }

        /// <summary>
        /// Gets the tags that can be inherited from a prior step in the workflow.
        /// </summary>
        public IReadOnlyDictionary<string, string> InheritableTags { get; private set; }
    }
}
