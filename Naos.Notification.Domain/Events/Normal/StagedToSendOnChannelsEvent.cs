// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StagedToSendOnChannelsEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The notification is staged to be sent on one or more channels.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class StagedToSendOnChannelsEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StagedToSendOnChannelsEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsResult">The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <param name="channelToPrepareToSendOnChannelResultMap">A map of channel to the result of executing a <see cref="PrepareToSendOnChannelOp"/>.</param>
        /// <param name="cannotStageToSendOnChannelAction">The action taken when the system could not stage a notification to be sent on a channel.</param>
        /// <param name="channelsToSendOn">The channels that the notification will be sent on.</param>
        public StagedToSendOnChannelsEvent(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            IReadOnlyDictionary<IDeliveryChannel, PrepareToSendOnChannelResult> channelToPrepareToSendOnChannelResultMap,
            CannotStageToSendOnChannelAction cannotStageToSendOnChannelAction,
            IReadOnlyCollection<IDeliveryChannel> channelsToSendOn)
            : base(id, timestampUtc)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();
            new { getDeliveryChannelConfigsResult }.AsArg().Must().NotBeNull();
            new { channelToPrepareToSendOnChannelResultMap }.AsArg().Must().NotBeNull().And().NotContainAnyKeyValuePairsWithNullValue();
            new { cannotStageToSendOnChannelAction }.AsArg().Must().NotBeEqualTo(CannotStageToSendOnChannelAction.Unknown);
            new { channelsToSendOn }.AsArg().Must().NotBeNullNorEmptyEnumerableNorContainAnyNulls();

            this.GetAudienceResult = getAudienceResult;
            this.GetDeliveryChannelConfigsResult = getDeliveryChannelConfigsResult;
            this.ChannelToPrepareToSendOnChannelResultMap = channelToPrepareToSendOnChannelResultMap;
            this.CannotStageToSendOnChannelAction = cannotStageToSendOnChannelAction;
            this.ChannelsToSendOn = channelsToSendOn;
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
        /// Gets a map of channel to the result of executing a <see cref="PrepareToSendOnChannelOp"/>.
        /// </summary>
        public IReadOnlyDictionary<IDeliveryChannel, PrepareToSendOnChannelResult> ChannelToPrepareToSendOnChannelResultMap { get; private set; }

        /// <summary>
        /// Gets the action taken when the system could not stage a notification to be sent on a channel.
        /// </summary>
        public CannotStageToSendOnChannelAction CannotStageToSendOnChannelAction { get; private set; }

        /// <summary>
        /// Gets the channels that the notification will be sent on.
        /// </summary>
        public IReadOnlyCollection<IDeliveryChannel> ChannelsToSendOn { get; private set; }
    }
}