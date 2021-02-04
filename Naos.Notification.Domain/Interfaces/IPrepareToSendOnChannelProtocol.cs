// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPrepareToSendOnChannelProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using Naos.Protocol.Domain;

    /// <summary>
    /// Executes a <see cref="PrepareToSendOnChannelOp"/>.
    /// </summary>
    public interface IPrepareToSendOnChannelProtocol : IAsyncReturningProtocol<PrepareToSendOnChannelOp, PrepareToSendOnChannelResult>
    {
    }
}
