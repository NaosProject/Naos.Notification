// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildTagsForEventOp{TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;

    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Builds the tags to use when putting an Event in to a Stream.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to build the tags for.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class BuildTagsForEventOp<TEvent> : ReturningOperationBase<IReadOnlyDictionary<string, string>>, IModelViaCodeGen
        where TEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTagsForEventOp{TEvent}"/> class.
        /// </summary>
        /// <param name="trackingCodeId">The notification tracking code identifier.</param>
        /// <param name="event">The event that will be put.</param>
        /// <param name="inheritableTags">OPTIONAL tags that can be inherited from a prior step in the workflow.  DEFAULT is null, indicating that there was no prior step or that no tags have been established in the workflow.</param>
        public BuildTagsForEventOp(
            long trackingCodeId,
            TEvent @event,
            IReadOnlyDictionary<string, string> inheritableTags = null)
        {
            new { @event }.AsArg().Must().NotBeNull();

            this.TrackingCodeId = trackingCodeId;
            this.Event = @event;
            this.InheritableTags = inheritableTags;
        }

        /// <summary>
        /// Gets the notification tracking code identifier.
        /// </summary>
        public long TrackingCodeId { get; private set; }

        /// <summary>
        /// Gets the event that will be put.
        /// </summary>
        public TEvent Event { get; private set; }

        /// <summary>
        /// Gets the tags that can be inherited from a prior step in the workflow.
        /// </summary>
        public IReadOnlyDictionary<string, string> InheritableTags { get; private set; }
    }
}
