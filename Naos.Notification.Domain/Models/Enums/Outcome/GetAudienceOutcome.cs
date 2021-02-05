// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAudienceOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of executing a <see cref="GetAudienceOp"/>.
    /// </summary>
    public enum GetAudienceOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The system got the audience and there were no failures reported.
        /// </summary>
        GotAudienceWithNoFailuresReported,

        /// <summary>
        /// The system got the audience.  There were failures reported but they were ignored.
        /// </summary>
        GotAudienceWithReportedFailuresIgnored,

        /// <summary>
        /// The system could not get the audience and there were no failures reported.
        /// </summary>
        CouldNotGetAudienceAndNoFailuresReported,

        /// <summary>
        /// The system could not get the audience and one or more failures were reported.
        /// </summary>
        CouldNotGetAudienceWithSomeFailuresReported,

        /// <summary>
        /// The system got the audience, but there were failures reported and these failures prevent the system from using the audience.
        /// </summary>
        DespiteGettingAudienceFailuresPreventUsingIt,
    }
}