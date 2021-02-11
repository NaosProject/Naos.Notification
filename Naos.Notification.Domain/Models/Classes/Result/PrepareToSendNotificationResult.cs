// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendNotificationResult.cs" company="Naos Project">
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
    /// The result of preparing to send a notification on all configured channels.
    /// </summary>
    public partial class PrepareToSendNotificationResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrepareToSendNotificationResult"/> class.
        /// </summary>
        /// <param name="channelToPrepareToSendOnChannelResultMap">A map of channel to the result of executing a <see cref="PrepareToSendOnChannelOp"/>.</param>
        /// <param name="cannotPrepareToSendOnChannelAction">The action taken when the system could not prepare the notification to be sent on a channel.</param>
        /// <param name="channelsToSendOn">The channels that the notification is prepared to be sent on.</param>
        public PrepareToSendNotificationResult(
            IReadOnlyDictionary<IDeliveryChannel, PrepareToSendOnChannelResult> channelToPrepareToSendOnChannelResultMap,
            CannotPrepareToSendOnChannelAction cannotPrepareToSendOnChannelAction,
            IReadOnlyCollection<IDeliveryChannel> channelsToSendOn)
        {
            new { channelToPrepareToSendOnChannelResultMap }.AsArg().Must().NotBeNull().And().NotContainAnyKeyValuePairsWithNullValue();
            var prepareToSendOnChannelResult = channelToPrepareToSendOnChannelResultMap?.Values.ToList();
            new { prepareToSendOnChannelResult }.AsArg().Must().ContainOnlyDistinctElementsWhenNotNull();
            new { cannotPrepareToSendOnChannelAction }.AsArg().Must().NotBeEqualTo(CannotPrepareToSendOnChannelAction.Unknown);
            new { channelsToSendOn }.AsArg().Must().NotBeNull().And().NotContainAnyNullElements().And().ContainOnlyDistinctElements();

            foreach (var channelToSendOn in channelsToSendOn)
            {
                new { channelToPrepareToSendOnChannelResultMap }.AsArg().Must().ContainKey(channelToSendOn);
            }

            this.ChannelToPrepareToSendOnChannelResultMap = channelToPrepareToSendOnChannelResultMap;
            this.CannotPrepareToSendOnChannelAction = cannotPrepareToSendOnChannelAction;
            this.ChannelsToSendOn = channelsToSendOn;
        }

        /// <summary>
        /// Gets a map of channel to the result of executing a <see cref="PrepareToSendOnChannelOp"/>.
        /// </summary>
        public IReadOnlyDictionary<IDeliveryChannel, PrepareToSendOnChannelResult> ChannelToPrepareToSendOnChannelResultMap { get; private set; }

        /// <summary>
        /// Gets the action taken when the system could not prepare the notification to be sent on a channel.
        /// </summary>
        public CannotPrepareToSendOnChannelAction CannotPrepareToSendOnChannelAction { get; private set; }

        /// <summary>
        /// Gets the channels that the notification is prepared to be sent on.
        /// </summary>
        public IReadOnlyCollection<IDeliveryChannel> ChannelsToSendOn { get; private set; }
    }
}