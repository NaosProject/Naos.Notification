// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBuildTagsForEvent{TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;

    using Naos.Protocol.Domain;

    /// <summary>
    /// Executes a <see cref="BuildTagsForEventOp{TEvent}"/>.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to build the tags for.</typeparam>
    public interface IBuildTagsForEvent<TEvent> : IAsyncReturningProtocol<BuildTagsForEventOp<TEvent>, IReadOnlyDictionary<string, string>>
        where TEvent : IEvent
    {
    }
}
