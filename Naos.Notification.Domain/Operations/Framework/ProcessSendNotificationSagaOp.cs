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
        /// <param name="channelToOperationsMonitoringInfoMap">A map of <see cref="IDeliveryChannel"/> to information needed to monitor the execution of the operations on those channels.</param>
        public ProcessSendNotificationSagaOp(
            long notificationTrackingCodeId,
            IReadOnlyDictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationMonitoringInfo>> channelToOperationsMonitoringInfoMap)
        {
            new { channelToOperationsMonitoringInfoMap }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();
            var perChannelOperationsMonitoringInfo = channelToOperationsMonitoringInfoMap.Values;
            new { perChannelOperationsMonitoringInfo }.AsArg().Must().Each().NotBeNullNorEmptyEnumerableNorContainAnyNulls();
            var channelOperationTrackingCodeIds = channelToOperationsMonitoringInfoMap.Values.Select(_ => _.Select(c => c.ChannelOperationTrackingCodeId).ToList()).ToList();
            new { channelOperationTrackingCodeIds }.AsArg().Must().Each().ContainOnlyDistinctElementsWhenNotNull();

            this.NotificationTrackingCodeId = notificationTrackingCodeId;
            this.ChannelToOperationsMonitoringInfoMap = channelToOperationsMonitoringInfoMap;
        }

        /// <summary>
        /// Gets the notification tracking code id.
        /// </summary>
        public long NotificationTrackingCodeId { get; private set; }

        /// <summary>
        /// Gets a map of <see cref="IDeliveryChannel"/> to information needed to monitor the execution of the operations on those channels.
        /// </summary>
        public IReadOnlyDictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationMonitoringInfo>> ChannelToOperationsMonitoringInfoMap { get; private set; }
    }
}