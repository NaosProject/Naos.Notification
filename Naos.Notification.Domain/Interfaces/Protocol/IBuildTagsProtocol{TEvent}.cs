// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBuildTagsProtocol{TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Executes a <see cref="BuildTagsOp{TEvent}"/>.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to build the tags for.</typeparam>
    public interface IBuildTagsProtocol<TEvent> : ISyncReturningProtocol<BuildTagsOp<TEvent>, IReadOnlyCollection<NamedValue<string>>>
        where TEvent : IEvent
    {
    }
}
