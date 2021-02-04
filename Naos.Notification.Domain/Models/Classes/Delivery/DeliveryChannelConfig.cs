// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeliveryChannelConfig.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Configures a <see cref="IDeliveryChannel"/> for a <see cref="DeliveryChannelAction"/>.
    /// </summary>
    public partial class DeliveryChannelConfig : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryChannelConfig"/> class.
        /// </summary>
        /// <param name="channel">The delivery channel.</param>
        /// <param name="action">The action to take on the delivery channel.</param>
        public DeliveryChannelConfig(
            IDeliveryChannel channel,
            DeliveryChannelAction action)
        {
            new { channel }.AsArg().Must().NotBeNull();
            new { action }.AsArg().Must().NotBeEqualTo(DeliveryChannelAction.Unknown);

            this.Channel = channel;
            this.Action = action;
        }

        /// <summary>
        /// Gets the delivery channel.
        /// </summary>
        public IDeliveryChannel Channel { get; private set; }

        /// <summary>
        /// Gets the action to take on the delivery channel.
        /// </summary>
        public DeliveryChannelAction Action { get; private set; }
    }
}