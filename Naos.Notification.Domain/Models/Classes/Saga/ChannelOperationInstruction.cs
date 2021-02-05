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
        /// <param name="operation">The channel operation.</param>
        /// <param name="outcomeSpec">The channel operation outcome specification.</param>
        /// <param name="tags">OPTIONAL tags to use when putting the operation into the channel-specific Operation Stream.  DEFAULT is to specify no tags.</param>
        public ChannelOperationInstruction(
            IOperation operation,
            ChannelOperationOutcomeSpec outcomeSpec,
            IReadOnlyDictionary<string, string> tags = null)
        {
            new { operation }.AsArg().Must().NotBeNull();
            new { outcomeSpec }.AsArg().Must().NotBeNull();

            this.Operation = operation;
            this.OutcomeSpec = outcomeSpec;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the channel operation.
        /// </summary>
        public IOperation Operation { get; private set; }

        /// <summary>
        /// Gets the channel operation outcome specification.
        /// </summary>
        public ChannelOperationOutcomeSpec OutcomeSpec { get; private set; }

        /// <summary>
        /// Gets the tags to use when putting the operation into the channel-specific Operation Stream.
        /// </summary>
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
