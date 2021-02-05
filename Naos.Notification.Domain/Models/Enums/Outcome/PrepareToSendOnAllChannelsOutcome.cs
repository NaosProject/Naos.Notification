// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendOnAllChannelsOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of preparing the notification to be sent on all configured channels.
    /// </summary>
    public enum PrepareToSendOnAllChannelsOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The audience opted out of delivery on all configured channels.
        /// </summary>
        AudienceOptedOutOfDeliveryOnAllChannels,

        /// <summary>
        /// All channels configured for sending were prepared to send on.
        /// </summary>
        AllChannelsWerePreparedToSendOn,

        /// <summary>
        /// Some, but not all of the channels configured for sending were prepared to send on.
        /// </summary>
        SomeChannelsWerePreparedToSendOn,

        /// <summary>
        /// None of the channels configured for sending could be prepared to send on.
        /// </summary>
        UnableToPrepareAnyChannelToSendOn,

        /// <summary>
        /// One of channels configured for sending could not be prepared to send on and this
        /// prevented sending on any channel.
        /// </summary>
        UnableToPrepareOneChannelToSendOnWhichPreventedSendingOnAnyChannel,
    }
}