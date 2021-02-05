// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendOnChannelResult.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The result of executing a <see cref="PrepareToSendOnChannelOp"/>.
    /// </summary>
    public partial class PrepareToSendOnChannelResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrepareToSendOnChannelResult"/> class.
        /// </summary>
        /// <param name="channelOperationInstructions">Ordered instructions for putting operations into a channel-specific Operation Stream and monitoring them for success/failure.</param>
        /// <param name="failures">The failures that occurred when executing the operation.</param>
        /// <param name="failureAction">The action to take when <paramref name="failures"/> contains one or more elements.</param>
        public PrepareToSendOnChannelResult(
            IReadOnlyList<ChannelOperationInstruction> channelOperationInstructions,
            IReadOnlyCollection<IFailure> failures,
            PrepareToSendOnChannelFailureAction failureAction)
        {
            new { channelOperationInstructions }.AsArg().Must().NotContainAnyNullElementsWhenNotNull();
            var trackingCodeIds = channelOperationInstructions?.Select(_ => _.TrackingCodeId).ToArray() ?? new long[0];
            new { trackingCodeIds }.AsArg().Must().ContainOnlyDistinctElements();
            new { failures }.AsArg().Must().NotContainAnyNullElementsWhenNotNull();
            new { failureAction }.AsArg().Must().NotBeEqualTo(PrepareToSendOnChannelFailureAction.Unknown);

            this.ChannelOperationInstructions = channelOperationInstructions;
            this.Failures = failures;
            this.FailureAction = failureAction;
        }

        /// <summary>
        /// Gets the ordered instructions for putting operations into a channel-specific Operation Stream and monitoring them for success/failure.
        /// </summary>
        public IReadOnlyList<ChannelOperationInstruction> ChannelOperationInstructions { get; private set; }

        /// <summary>
        /// Gets the failures that occurred when executing the operation.
        /// </summary>
        public IReadOnlyCollection<IFailure> Failures { get; private set; }

        /// <summary>
        /// Gets the action to take when <see cref="Failures"/> contains one or more elements.
        /// </summary>
        public PrepareToSendOnChannelFailureAction FailureAction { get; private set; }
    }
}