// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeliveryChannelBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="IDeliveryChannel"/>.
    /// </summary>
    /// <remarks>
    /// Derivatives should NOT include credentials as instance members.
    /// This is not meant to be a configuration object, but rather simply to
    /// define a delivery channel.
    /// </remarks>
    public abstract partial class DeliveryChannelBase : IDeliveryChannel, IModelViaCodeGen
    {
    }
}