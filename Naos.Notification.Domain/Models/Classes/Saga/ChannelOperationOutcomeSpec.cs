// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelOperationOutcomeSpec.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Specifies the outcome of a channel operation (e.g. SendEmailOp) as the presence of a specific
    /// event that indicates that the operation succeeded (e.g. SucceededInSendingEmailEvent) or a specific (but different)
    /// event that indicates that the operation failed (e.g. FailedToSendEmailEvent).
    /// </summary>
    public partial class ChannelOperationOutcomeSpec : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationOutcomeSpec"/> class.
        /// </summary>
        /// <param name="channelTrackingCodeId">The tracking code identifier for the channel operation.</param>
        /// <param name="succeededEventType">The event type that indicates that the operation succeeded.</param>
        /// <param name="failedEventType">The event type that indicates that the operation failed.</param>
        public ChannelOperationOutcomeSpec(
            long channelTrackingCodeId,
            TypeRepresentation succeededEventType,
            TypeRepresentation failedEventType)
        {
            new { succeededEventType }.AsArg().Must().NotBeNull();
            new { failedEventType }.AsArg().Must().NotBeNull().And().NotBeEqualTo(succeededEventType);

            this.ChannelTrackingCodeId = channelTrackingCodeId;
            this.SucceededEventType = succeededEventType;
            this.FailedEventType = failedEventType;
        }

        /// <summary>
        /// Gets the tracking code identifier for the channel operation.
        /// </summary>
        public long ChannelTrackingCodeId { get; private set; }

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