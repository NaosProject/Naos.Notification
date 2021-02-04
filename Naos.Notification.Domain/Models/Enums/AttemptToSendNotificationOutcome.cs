// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttemptToSendNotificationOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of an attempt to send a notification on all prepared channels.
    /// </summary>
    /// <remarks>
    /// The notification could have failed to be prepared for some channels.
    /// Further, the recipient might have opted-out of some channels.
    /// This outcome is scoped to the channels that the notification was successfully prepared for.
    /// </remarks>
    public enum AttemptToSendNotificationOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The notification was successfully sent on all channels.
        /// </summary>
        SucceededOnAllChannels,

        /// <summary>
        /// The notification failed to be sent on some channels and was successfully sent on some channels
        /// Within all channels, all operations either failed or succeeded (no partial sends on any channels).
        /// </summary>
        MixedWithFullChannelSends,

        /// <summary>
        /// The notification failed to be sent on some channels and was successfully sent on some channels.
        /// Within at least one channel there were operations that failed and succeeded (partial sends on one or more channels).
        /// </summary>
        MixedWithPartialChannelSends,

        /// <summary>
        /// The notification failed to be sent on all channels.
        /// </summary>
        FailedOnAllChannels,
    }
}
