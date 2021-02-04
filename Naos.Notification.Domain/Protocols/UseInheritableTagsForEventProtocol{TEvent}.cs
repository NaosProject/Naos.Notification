﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UseInheritableTagsForEventProtocol{TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Extension methods on protocols.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to build the tags for.</typeparam>
    public class UseInheritableTagsForEventProtocol<TEvent> : AsyncSpecificReturningProtocolBase<BuildTagsForEventOp<TEvent>, IReadOnlyDictionary<string, string>>
        where TEvent : IEvent
    {
        /// <inheritdoc />
        public override async Task<IReadOnlyDictionary<string, string>> ExecuteAsync(
            BuildTagsForEventOp<TEvent> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            var tags = operation.InheritableTags;

            var result = await Task.FromResult(tags);

            return result;
        }
    }
}