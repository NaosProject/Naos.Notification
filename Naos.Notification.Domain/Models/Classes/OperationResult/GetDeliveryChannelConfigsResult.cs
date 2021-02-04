// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetDeliveryChannelConfigsResult.cs" company="Naos Project">
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
    /// The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.
    /// </summary>
    public partial class GetDeliveryChannelConfigsResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDeliveryChannelConfigsResult"/> class.
        /// </summary>
        /// <param name="configs">The delivery channel configs.</param>
        /// <param name="failures">The failures that occurred when executing the operation.</param>
        /// <param name="failureAction">The action to take when <paramref name="failures"/> contains one or more elements.</param>
        public GetDeliveryChannelConfigsResult(
            IReadOnlyCollection<DeliveryChannelConfig> configs,
            IReadOnlyCollection<IFailure> failures,
            FailureAction failureAction)
        {
            new { configs }.AsArg().Must().NotContainAnyNullElementsWhenNotNull();
            var deliveryChannels = configs.Select(_ => _.Channel).ToList();
            new { deliveryChannels }.AsArg().Must().ContainOnlyDistinctElementsWhenNotNull();
            new { failures }.AsArg().Must().NotContainAnyNullElementsWhenNotNull();
            new { failureAction }.AsArg().Must().NotBeEqualTo(FailureAction.Unknown);

            this.Configs = configs;
            this.Failures = failures;
            this.FailureAction = failureAction;
        }

        /// <summary>
        /// Gets the delivery channel configs.
        /// </summary>
        public IReadOnlyCollection<DeliveryChannelConfig> Configs { get; private set; }

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