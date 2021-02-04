// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationOutcomeSpec.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Specifies the outcome of an operation as the presence of a specific
    /// event that indicates that the operation succeeded or a specific (but different)
    /// event that indicates that the operation failed.
    /// </summary>
    public partial class OperationOutcomeSpec : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationOutcomeSpec"/> class.
        /// </summary>
        /// <param name="eventId">The event identifier to filter on.</param>
        /// <param name="succeededEventType">The event type that indicates that the operation succeeded.</param>
        /// <param name="failedEventType">The event type that indicates that the operation failed.</param>
        public OperationOutcomeSpec(
            long eventId,
            TypeRepresentation succeededEventType,
            TypeRepresentation failedEventType)
        {
            new { succeededEventType }.AsArg().Must().NotBeNull();
            new { failedEventType }.AsArg().Must().NotBeNull().And().NotBeEqualTo(succeededEventType);

            this.EventId = eventId;
            this.SucceededEventType = succeededEventType;
            this.FailedEventType = failedEventType;
        }

        /// <summary>
        /// Gets the event identifier to filter on.
        /// </summary>
        public long EventId { get; private set; }

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