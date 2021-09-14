// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddToInheritableTagsProtocol{TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Cloning.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Executes a <see cref="BuildTagsOp{TEvent}" /> and returns the <see cref="BuildTagsOp{TEvent}.InheritableTags"/>.
    /// </summary>
    /// <remarks>
    /// This is useful when you an event to be tagged with the inheritable tags and do not want to augment that set.
    /// </remarks>
    /// <typeparam name="TEvent">The type of event to build the tags for.</typeparam>
    public class AddToInheritableTagsProtocol<TEvent> : SyncSpecificReturningProtocolBase<BuildTagsOp<TEvent>, IReadOnlyCollection<NamedValue<string>>>, IBuildTagsProtocol<TEvent>
        where TEvent : IEvent
    {
        private readonly Func<TEvent, IReadOnlyCollection<NamedValue<string>>> getTagsToAdd;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToInheritableTagsProtocol{TEvent}"/> class.
        /// </summary>
        /// <param name="getTagsToAdd">A func taking <typeparamref name="TEvent"/> and returning a set of tags to add to the inheritable tags.</param>
        public AddToInheritableTagsProtocol(
            Func<TEvent, IReadOnlyCollection<NamedValue<string>>> getTagsToAdd)
        {
            new { getTagsToAdd }.AsArg().Must().NotBeNull();

            this.getTagsToAdd = getTagsToAdd;
        }

        /// <inheritdoc />
        public override IReadOnlyCollection<NamedValue<string>> Execute(
            BuildTagsOp<TEvent> operation)
        {
            new { operation }.AsArg().Must().NotBeNull();

            var result = (operation.InheritableTags?.DeepClone() ?? new List<NamedValue<string>>()).ToList();

            var tagsToAdd = this.getTagsToAdd(operation.Event);

            foreach (var tagToAdd in tagsToAdd)
            {
                result.Add(new NamedValue<string>(tagToAdd.Name, tagToAdd.Value));
            }

            return result;
        }
    }
}
