// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelOperationOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of executing a channel-specific operation (e.g. SendEmailOp).
    /// </summary>
    public enum ChannelOperationOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The channel operation succeeded.
        /// </summary>
        Succeeded,

        /// <summary>
        /// The channel operation failed.
        /// </summary>
        Failed,
    }
}
