// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FailureAction.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The action to take when a protocol returns a result object containing one or more failures.
    /// </summary>
    public enum FailureAction
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Ignore all failures and proceed to the next step if possible
        /// (i.e. the result object contains the necessary information to proceed).
        /// If not possible, then stop.
        /// </summary>
        IgnoreAndProceedIfPossibleOtherwiseStop,

        /// <summary>
        /// Stop if there are any failures,
        /// regardless of whether it is possible to proceed to the next step
        /// (i.e. the result object contains the necessary information to proceed).
        /// </summary>
        Stop,
    }
}