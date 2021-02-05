// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreparedToSendNotificationEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The notification has been prepared to be sent on one or more channels.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PreparedToSendNotificationEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreparedToSendNotificationEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsResult">The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <param name="prepareToSendOnAllChannelsResult">The result of preparing to send the notification on all configured channels.</param>
        public PreparedToSendNotificationEvent(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            PrepareToSendOnAllChannelsResult prepareToSendOnAllChannelsResult)
            : base(id, timestampUtc)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();
            var getAudienceOutcome = getAudienceResult.GetOutcome();
            new { getAudienceOutcome }.AsArg().Must().NotBeEqualTo(GetAudienceOutcome.GotAudienceWithNoFailuresReported).And().NotBeEqualTo(GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored);

            new { getDeliveryChannelConfigsResult }.AsArg().Must().NotBeNull();
            var getDeliveryChannelConfigsOutcome = getDeliveryChannelConfigsResult.GetOutcome();
            new { getDeliveryChannelConfigsOutcome }.AsArg().Must().NotBeEqualTo(GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithNoFailuresReported).And().NotBeEqualTo(GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithReportedFailuresIgnored);

            new { prepareToSendOnAllChannelsResult }.AsArg().Must().NotBeNull();
            var prepareToSendOnAllChannelsOutcome = prepareToSendOnAllChannelsResult.GetOutcome();
            new { prepareToSendOnAllChannelsOutcome }.AsArg().Must().NotBeEqualTo(PrepareToSendOnAllChannelsOutcome.AudienceOptedOutOfDeliveryOnAllChannels).And().NotBeEqualTo(PrepareToSendOnAllChannelsOutcome.UnableToPrepareAnyChannelToSendOn).And().NotBeEqualTo(PrepareToSendOnAllChannelsOutcome.UnableToPrepareOneChannelToSendOnWhichPreventedSendingOnAnyChannel);

            this.GetAudienceResult = getAudienceResult;
            this.GetDeliveryChannelConfigsResult = getDeliveryChannelConfigsResult;
            this.PrepareToSendOnAllChannelsResult = prepareToSendOnAllChannelsResult;
        }

        /// <summary>
        /// Gets the result of executing a <see cref="GetAudienceOp"/>.
        /// </summary>
        public GetAudienceResult GetAudienceResult { get; private set; }

        /// <summary>
        /// Gets the result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.
        /// </summary>
        public GetDeliveryChannelConfigsResult GetDeliveryChannelConfigsResult { get; private set; }

        /// <summary>
        /// Gets the result of preparing to send the notification on all configured channels.
        /// </summary>
        public PrepareToSendOnAllChannelsResult PrepareToSendOnAllChannelsResult { get; private set; }
    }
}