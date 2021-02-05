// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetDeliveryChannelConfigsOutcome.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// The outcome of executing a <see cref="GetDeliveryChannelConfigsOp"/>.
    /// </summary>
    public enum GetDeliveryChannelConfigsOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The system got the delivery channel configs and there were no failures reported.
        /// </summary>
        GotDeliveryChannelConfigsWithNoFailuresReported,

        /// <summary>
        /// The system got the delivery channel configs.  There were failures reported but they were ignored.
        /// </summary>
        GotDeliveryChannelConfigsWithReportedFailuresIgnored,

        /// <summary>
        /// The system could not get the delivery channel configs and there were no failures reported.
        /// </summary>
        CouldNotGetDeliveryChannelConfigsAndNoFailuresReported,

        /// <summary>
        /// The system could not get the delivery channel configs and one or more failures were reported.
        /// </summary>
        CouldNotGetDeliveryChannelConfigsWithSomeFailuresReported,

        /// <summary>
        /// The system got the delivery channel configs, but there were failures reported and these failures prevent the system from using the channel configs.
        /// </summary>
        DespiteGettingDeliveryChannelConfigsFailuresPreventUsingThem,
    }
}