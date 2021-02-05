// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendOnChannelFailureAction.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The action to take when <see cref="PrepareToSendOnChannelResult"/> contains one or more failures.
    /// </summary>
    public enum PrepareToSendOnChannelFailureAction
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Ignore all failures and treat the channel as prepared to sending on, if possible
        /// (i.e. the result object contains one or more <see cref="ChannelOperationInstruction"/>).
        /// If not possible, then do not send on the channel.
        /// </summary>
        IgnoreAndTreatChannelAsPreparedToSendOnIfPossible,

        /// <summary>
        /// Do NOT send on the channel,
        /// regardless of whether it is possible to do so
        /// (i.e. the result object contains one or more <see cref="ChannelOperationInstruction"/>).
        /// </summary>
        DoNotSendOnChannel,
    }
}