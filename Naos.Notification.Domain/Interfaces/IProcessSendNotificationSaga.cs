// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProcessSendNotificationSaga.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using Naos.Protocol.Domain;

    /// <summary>
    /// Executes a <see cref="ProcessSendNotificationSagaOp"/>.
    /// </summary>
    public interface IProcessSendNotificationSaga : IAsyncVoidProtocol<ProcessSendNotificationSagaOp>
    {
    }
}
