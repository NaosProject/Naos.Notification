// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttemptToSendNotificationOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of attempting to send a notification on all prepared channels.
    /// </summary>
    /// <remarks>
    /// The notification could have failed to be prepared to send on some channels.
    /// Further, the recipient might have opted-out of some channels.
    /// This outcome is scoped to the channels that the notification was successfully prepared to send on.
    /// Further, the notification is considered sent on a channel if ALL channel-operations succeeded.
    /// </remarks>
    public enum AttemptToSendNotificationOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The notification was sent on all channels that it was prepared to be sent on.
        /// </summary>
        SentOnAllPreparedChannels,

        /// <summary>
        /// The notification failed to be sent on some channels that it was prepared to be sent on
        /// and was successfully sent on the other channels it was prepared to be sent on.
        /// </summary>
        SentOnSomePreparedChannels,

        /// <summary>
        /// The notification failed to be sent on any channel it was prepared to be sent on.
        /// </summary>
        CouldNotSendOnAnyPreparedChannel,
    }
}
