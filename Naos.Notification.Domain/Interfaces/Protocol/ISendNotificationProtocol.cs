// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISendNotificationProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Executes a <see cref="SendNotificationOp"/>.
    /// </summary>
    public interface ISendNotificationProtocol : IAsyncReturningProtocol<SendNotificationOp, NotificationTrackingCode>
    {
    }
}
