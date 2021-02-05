// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendOnChannelOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of executing a <see cref="PrepareToSendOnChannelOp"/>.
    /// </summary>
    public enum PrepareToSendOnChannelOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The system prepared to send on the channel and there were no failures reported.
        /// </summary>
        PreparedToSendOnChannelWithNoFailuresReported,

        /// <summary>
        /// The system prepared to send on the channel.  There were failures reported but they were ignored.
        /// </summary>
        PreparedToSendOnChannelWithReportedFailuresIgnored,

        /// <summary>
        /// The system could not prepare to send on the channel and there were no failures reported.
        /// </summary>
        CouldNotPrepareToSendOnChannelAndNoFailuresReported,

        /// <summary>
        /// The system could not prepare to send on the channel and one or more failures were reported.
        /// </summary>
        CouldNotPrepareToSendOnChannelWithSomeFailuresReported,

        /// <summary>
        /// The system prepared to send on the channel, but there were failures reported and these failures prevent the system from using the channel.
        /// </summary>
        DespitePreparingToSendOnChannelFailuresPreventUsingIt,
    }
}