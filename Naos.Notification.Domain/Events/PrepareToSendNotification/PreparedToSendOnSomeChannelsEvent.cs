// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreparedToSendOnSomeChannelsEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The system prepared to send the notification on some, but not all of the channels configured for sending.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PreparedToSendOnSomeChannelsEvent : PrepareToSendNotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreparedToSendOnSomeChannelsEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsResult">The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <param name="prepareToSendNotificationResult">The result of preparing to send the notification on all configured channels.</param>
        public PreparedToSendOnSomeChannelsEvent(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            PrepareToSendNotificationResult prepareToSendNotificationResult)
            : base(id, timestampUtc, getAudienceResult, getDeliveryChannelConfigsResult, prepareToSendNotificationResult)
        {
            var prepareToSendNotificationOutcome = prepareToSendNotificationResult.GetOutcome();
            new { prepareToSendNotificationOutcome }.AsArg().Must().BeEqualTo(PrepareToSendNotificationOutcome.PreparedToSendOnSomeChannels);
        }
    }
}