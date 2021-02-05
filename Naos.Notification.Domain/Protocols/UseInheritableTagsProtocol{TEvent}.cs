﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UseInheritableTagsProtocol{TEvent}.cs" company="Naos Project">
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
    /// Executes a <see cref="BuildTagsOp{TEvent}" /> and returns the <see cref="BuildTagsOp{TEvent}.InheritableTags"/>.
    /// </summary>
    /// <remarks>
    /// This is useful when you an event to be tagged with the inheritable tags and do not want to augment that set.
    /// </remarks>
    /// <typeparam name="TEvent">The type of event to build the tags for.</typeparam>
    public class UseInheritableTagsProtocol<TEvent> : AsyncSpecificReturningProtocolBase<BuildTagsOp<TEvent>, IReadOnlyDictionary<string, string>>, IBuildTagsProtocol<TEvent>
        where TEvent : IEvent
    {
        /// <inheritdoc />
        public override async Task<IReadOnlyDictionary<string, string>> ExecuteAsync(
            BuildTagsOp<TEvent> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            var tags = operation.InheritableTags;

            var result = await Task.FromResult(tags);

            return result;
        }
    }
}
