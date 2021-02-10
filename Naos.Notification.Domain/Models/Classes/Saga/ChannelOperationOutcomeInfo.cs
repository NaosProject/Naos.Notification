// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelOperationOutcomeInfo.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Contains information about the outcome of executing a channel-specific operation (e.g. SendEmailOp).
    /// </summary>
    /// <remarks>
    /// The operation could be the only operation or one of many on the channel for the same notification.
    /// Likewise, the notification might be sent on one or many channels.
    /// This class is scoped to a single operation on a single channel.
    /// </remarks>
    public partial class ChannelOperationOutcomeInfo : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationOutcomeInfo"/> class.
        /// </summary>
        /// <param name="channelOperationTrackingCodeId">The tracking code identifier for the channel operation.</param>
        /// <param name="eventType">The type of event that specified the outcome.</param>
        /// <param name="outcome">The outcome of executing the operation.</param>
        public ChannelOperationOutcomeInfo(
            long channelOperationTrackingCodeId,
            TypeRepresentation eventType,
            ChannelOperationOutcome outcome)
        {
            new { eventType }.AsArg().Must().NotBeNull();
            new { outcome }.AsArg().Must().NotBeEqualTo(ChannelOperationOutcome.Unknown);

            this.ChannelOperationTrackingCodeId = channelOperationTrackingCodeId;
            this.EventType = eventType;
            this.Outcome = outcome;
        }

        /// <summary>
        /// Gets the tracking code identifier for the channel operation.
        /// </summary>
        public long ChannelOperationTrackingCodeId { get; private set; }

        /// <summary>
        /// Gets the type of event that specified the outcome.
        /// </summary>
        public TypeRepresentation EventType { get; private set; }

        /// <summary>
        /// Gets the outcome of executing the operation.
        /// </summary>
        public ChannelOperationOutcome Outcome { get; private set; }
    }
}