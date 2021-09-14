// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetAudienceProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Executes a <see cref="GetAudienceOp"/>.
    /// </summary>
    public interface IGetAudienceProtocol : IAsyncReturningProtocol<GetAudienceOp, GetAudienceResult>
    {
    }
}
