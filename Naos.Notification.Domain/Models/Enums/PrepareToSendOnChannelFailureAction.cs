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
        /// Ignore all failures and stage the notification for sending on the channel if possible
        /// (i.e. the result object contains one or more <see cref="ChannelOperationInstruction"/>).
        /// If not possible, then do not stage the notification for sending on the channel.
        /// </summary>
        IgnoreAndStageForSendingOnChannel,

        /// <summary>
        /// Do not stage the notification for sending on the channel,
        /// regardless of whether it is possible to do so
        /// (i.e. the result object contains one or more <see cref="ChannelOperationInstruction"/>).
        /// </summary>
        DoNotStageForSendingOnChannel,
    }
}