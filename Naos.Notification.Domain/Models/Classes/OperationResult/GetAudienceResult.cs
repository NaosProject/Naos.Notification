// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAudienceResult.cs" company="Naos Project">
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
    /// The result of executing a <see cref="GetAudienceOp"/>.
    /// </summary>
    public partial class GetAudienceResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAudienceResult"/> class.
        /// </summary>
        /// <param name="audience">The resolved audience.</param>
        /// <param name="failures">The failures that occurred when executing the operation.</param>
        /// <param name="failureAction">The action to take when <paramref name="failures"/> contains one or more elements.</param>
        public GetAudienceResult(
            IAudience audience,
            IReadOnlyCollection<IFailure> failures,
            FailureAction failureAction)
        {
            new { failures }.AsArg().Must().NotContainAnyNullElementsWhenNotNull();
            new { failureAction }.AsArg().Must().NotBeEqualTo(FailureAction.Unknown);

            this.Audience = audience;
            this.Failures = failures;
            this.FailureAction = failureAction;
        }

        /// <summary>
        /// Gets the resolved audience.
        /// </summary>
        public IAudience Audience { get; private set; }

        /// <summary>
        /// Gets the failures that occurred when executing the operation.
        /// </summary>
        public IReadOnlyCollection<IFailure> Failures { get; private set; }

        /// <summary>
        /// Gets the action to take when <see cref="Failures"/> contains one or more elements.
        /// </summary>
        public FailureAction FailureAction { get; private set; }
    }
}