// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CannotStageToSendOnChannelAction.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The action to take when the system cannot stage a notification to be sent on a channel
    /// (could be one of many configured channels or the only configured channel).
    /// </summary>
    public enum CannotStageToSendOnChannelAction
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Stop. If the notification cannot be staged to send
        /// on a single channel then do not send on any channel.
        /// </summary>
        StopAndNotDoNotSendOnAnyChannels,

        /// <summary>
        /// Continue and attempt staging to send on the next channel.
        /// </summary>
        ContinueAndAttemptStagingToSendOnNextChannel,
    }
}