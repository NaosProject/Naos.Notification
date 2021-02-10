// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelOperationMonitoringInfo.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Contains information needed to monitor the execution of a channel-specific operation (e.g. SendEmailOp).
    /// </summary>
    /// <remarks>
    /// This is defined as an event that indicates that the operation succeeded (e.g. SucceededInSendingEmailEvent)
    /// and a (different) event that indicates that the operation failed (e.g. FailedToSendEmailEvent).
    /// The operation is considered to have not yet been executed if neither of these events
    /// (having the specified tracking code id) are present.
    /// </remarks>
    public partial class ChannelOperationMonitoringInfo : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationMonitoringInfo"/> class.
        /// </summary>
        /// <param name="channelOperationTrackingCodeId">The tracking code identifier for the channel operation.</param>
        /// <param name="succeededEventType">The event type that indicates that the operation succeeded.</param>
        /// <param name="failedEventType">The event type that indicates that the operation failed.</param>
        public ChannelOperationMonitoringInfo(
            long channelOperationTrackingCodeId,
            TypeRepresentation succeededEventType,
            TypeRepresentation failedEventType)
        {
            new { succeededEventType }.AsArg().Must().NotBeNull();
            new { failedEventType }.AsArg().Must().NotBeNull().And().NotBeEqualTo(succeededEventType);

            this.ChannelOperationTrackingCodeId = channelOperationTrackingCodeId;
            this.SucceededEventType = succeededEventType;
            this.FailedEventType = failedEventType;
        }

        /// <summary>
        /// Gets the tracking code identifier for the channel operation.
        /// </summary>
        public long ChannelOperationTrackingCodeId { get; private set; }

        /// <summary>
        /// Gets the event type that indicates that the operation succeeded.
        /// </summary>
        public TypeRepresentation SucceededEventType { get; private set; }

        /// <summary>
        /// Gets the event type that indicates that the operation failed.
        /// </summary>
        public TypeRepresentation FailedEventType { get; private set; }
    }
}