// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendNotificationOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of preparing the notification to be sent on the configured channels.
    /// </summary>
    public enum PrepareToSendNotificationOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The audience opted out of delivery on all configured channels.
        /// </summary>
        AudienceOptedOutOfAllChannels,

        /// <summary>
        /// The system prepared to send the notification on all channels configured for sending.
        /// </summary>
        PreparedToSendOnAllChannels,

        /// <summary>
        /// The system prepared to send the notification on some, but not all of the channels configured for sending.
        /// </summary>
        PreparedToSendOnSomeChannels,

        /// <summary>
        /// The system could not prepare to send the notification on any channel configured for sending, despite attempting to do so for all of them.
        /// </summary>
        CouldNotPrepareToSendOnAnyChannelDespiteAttemptingAll,

        /// <summary>
        /// The system could not prepare to send the notification on any channel configured for sending because
        /// one of the channels could not be prepared to send the notification on and this prevented sending on any channel.
        /// </summary>
        CouldNotPrepareToSendOnAnyChannelBecauseOneForcedAllToBeDiscarded,
    }
}