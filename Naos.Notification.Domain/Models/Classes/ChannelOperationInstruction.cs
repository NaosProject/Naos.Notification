// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelOperationInstruction.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;

    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Contains the information needed to put an operation into a channel-specific
    /// Operation Stream (e.g. SendEmailOp) and monitor it for success/failure.
    /// </summary>
    public partial class ChannelOperationInstruction : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationInstruction"/> class.
        /// </summary>
        /// <param name="trackingCodeId">The operation's tracking code identifier.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="succeededEventType">The event type that indicates that the operation succeeded.</param>
        /// <param name="failedEventType">The event type that indicates that the operation failed.</param>
        /// <param name="tags">The tags to put.</param>
        public ChannelOperationInstruction(
            long trackingCodeId,
            IOperation operation,
            TypeRepresentation succeededEventType,
            TypeRepresentation failedEventType,
            IReadOnlyDictionary<string, string> tags)
        {
            new { operation }.AsArg().Must().NotBeNull();
            new { succeededEventType }.AsArg().Must().NotBeNull();
            new { failedEventType }.AsArg().Must().NotBeNull();
            new { tags }.AsArg().Must().NotBeNull();

            this.TrackingCodeId = trackingCodeId;
            this.Operation = operation;
            this.SucceededEventType = succeededEventType;
            this.FailedEventType = failedEventType;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the operation's tracking code identifier..
        /// </summary>
        public long TrackingCodeId { get; private set; }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        public IOperation Operation { get; private set; }

        /// <summary>
        /// Gets the event type that indicates that the operation succeeded.
        /// </summary>
        public TypeRepresentation SucceededEventType { get; private set; }

        /// <summary>
        /// Gets the event type that indicates that the operation failed.
        /// </summary>
        public TypeRepresentation FailedEventType { get; private set; }

        /// <summary>
        /// Gets the tags to put.
        /// </summary>
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
