// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreparedToSendOnChannelsEvent.cs" company="Naos Project">
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
    public partial class PreparedToSendOnChannelsEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreparedToSendOnChannelsEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsResult">The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <param name="prepareToSendOnAllChannelsResult">The result of preparing to send the notification on all configured channels.</param>
        public PreparedToSendOnChannelsEvent(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            PrepareToSendOnAllChannelsResult prepareToSendOnAllChannelsResult)
            : base(id, timestampUtc)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();
            new { getDeliveryChannelConfigsResult }.AsArg().Must().NotBeNull();
            new { prepareToSendOnAllChannelsResult }.AsArg().Must().NotBeNull();

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