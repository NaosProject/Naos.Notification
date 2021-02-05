// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeliveryChannelAction.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The action to take on a <see cref="IDeliveryChannel"/>.
    /// </summary>
    public enum DeliveryChannelAction
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Send a notification on the channel.
        /// </summary>
        SendOnChannel,

        /// <summary>
        /// The audience for the notification opted-out of delivery on the channel.
        /// </summary>
        AudienceOptedOut,
    }
}