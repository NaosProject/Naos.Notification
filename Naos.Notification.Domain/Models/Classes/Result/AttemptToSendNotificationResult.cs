// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttemptToSendNotificationResult.cs" company="Naos Project">
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
    /// The result of attempting to send the notification on all channels that the notification was prepared to send on.
    /// </summary>
    public partial class AttemptToSendNotificationResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttemptToSendNotificationResult"/> class.
        /// </summary>
        /// <param name="channelToOperationsOutcomeInfoMap">A map of delivery channel to the outcome of executing one or more channel-specific operations required to send the notification.</param>
        public AttemptToSendNotificationResult(
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyCollection<ChannelOperationOutcomeInfo>> channelToOperationsOutcomeInfoMap)
        {
            new { channelToOperationsOutcomeInfoMap }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();
            var perChannelOperationsOutcomeInfo = channelToOperationsOutcomeInfoMap.Values.ToList();
            new { perChannelOperationsOutcomeInfo }.AsArg().Must().Each().NotBeNullNorEmptyEnumerableNorContainAnyNulls();
            var perChannelOperationTrackingCodeIds = perChannelOperationsOutcomeInfo.Select(_ => _.Select(c => c.ChannelOperationTrackingCodeId).ToList()).ToList();
            new { perChannelOperationTrackingCodeIds }.AsArg().Must().Each().ContainOnlyDistinctElements();

            this.ChannelToOperationsOutcomeInfoMap = channelToOperationsOutcomeInfoMap;
        }

        /// <summary>
        /// Gets a map of delivery channel to the outcome of executing one or more channel-specific operations required to send the notification.
        /// </summary>
        public IReadOnlyDictionary<IDeliveryChannel, IReadOnlyCollection<ChannelOperationOutcomeInfo>> ChannelToOperationsOutcomeInfoMap { get; private set; }
    }
}