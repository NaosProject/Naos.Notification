// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessSendNotificationSagaOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Processes a saga that waits for channel-specific operations to complete
    /// and writes to the Notification Event Stream based on the outcome of those operations.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ProcessSendNotificationSagaOp : VoidOperationBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessSendNotificationSagaOp"/> class.
        /// </summary>
        /// <param name="notificationTrackingCodeId">The notification tracking code id.</param>
        /// <param name="channelToOperationOutcomeSpecsMap">A a map of <see cref="IDeliveryChannel"/> to the objects that specify whether the operations on that channel succeeded or failed.</param>
        public ProcessSendNotificationSagaOp(
            long notificationTrackingCodeId,
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationOutcomeSpec>> channelToOperationOutcomeSpecsMap)
        {
            new { channelToOperationOutcomeSpecsMap }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();
            new { channelToOperationOutcomeSpecsMap.Values }.AsArg(Invariant($"{nameof(channelToOperationOutcomeSpecsMap)}.Values")).Must().Each().NotBeNullNorEmptyEnumerableNorContainAnyNulls();
            var channelTrackingCodeIds = channelToOperationOutcomeSpecsMap.Values.Select(_ => _.Select(c => c.ChannelTrackingCodeId).ToList()).ToList();
            new { channelTrackingCodeIds }.AsArg().Must().Each().ContainOnlyDistinctElementsWhenNotNull();

            this.NotificationTrackingCodeId = notificationTrackingCodeId;
            this.ChannelToOperationOutcomeSpecsMap = channelToOperationOutcomeSpecsMap;
        }

        /// <summary>
        /// Gets the notification tracking code id.
        /// </summary>
        public long NotificationTrackingCodeId { get; private set; }

        /// <summary>
        /// Gets a map of <see cref="IDeliveryChannel"/> to the objects that specify whether the operations on that channel succeeded or failed.
        /// </summary>
        public IReadOnlyDictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationOutcomeSpec>> ChannelToOperationOutcomeSpecsMap { get; private set; }
    }
}