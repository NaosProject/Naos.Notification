// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CannotPrepareToSendOnChannelAction.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The action to take when the system cannot prepare a notification to be sent on a channel
    /// (could be one of many configured channels or the only configured channel).
    /// </summary>
    public enum CannotPrepareToSendOnChannelAction
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Stop. If the notification cannot be prepared to send
        /// on a single channel then do not send on any channel.
        /// </summary>
        StopAndNotDoNotSendOnAnyChannels,

        /// <summary>
        /// Continue and attempt preparing to send on the next channel.
        /// </summary>
        ContinueAndAttemptPreparingToSendOnNextChannel,
    }
}