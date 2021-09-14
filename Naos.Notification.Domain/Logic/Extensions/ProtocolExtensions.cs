// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolExtensions.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Extension methods on protocols.
    /// </summary>
    public static class ProtocolExtensions
    {
        /// <summary>
        /// Executes an <see cref="IBuildTagsProtocol{TEvent}"/> protocol.
        /// </summary>
        /// <remarks>
        /// This is a convenience method to build a <see cref="BuildTagsOp{TEvent}"/>
        /// and execute it against the specified protocol.
        /// </remarks>
        /// <typeparam name="TEvent">The type of event to build the tags for.</typeparam>
        /// <param name="protocol">The protocol to execute.</param>
        /// <param name="trackingCodeId">The tracking code identifier (in-context of the protocol putting the event).</param>
        /// <param name="event">The event that will be put.</param>
        /// <param name="inheritableTags">OPTIONAL tags that can be inherited from a prior step in the workflow.  DEFAULT is null, indicating that there was no prior step or that no tags have been established in the workflow.</param>
        /// <returns>
        /// The tags to use when putting the specified event into a stream.
        /// </returns>
        public static IReadOnlyCollection<NamedValue<string>> ExecuteBuildTags<TEvent>(
            this ISyncReturningProtocol<BuildTagsOp<TEvent>, IReadOnlyCollection<NamedValue<string>>> protocol,
            long trackingCodeId,
            TEvent @event,
            IReadOnlyCollection<NamedValue<string>> inheritableTags = null)
            where TEvent : IEvent
        {
            IReadOnlyCollection<NamedValue<string>> result = null;

            if (protocol != null)
            {
                var buildTagsOperation = new BuildTagsOp<TEvent>(trackingCodeId, @event, inheritableTags);

                result = protocol.Execute(buildTagsOperation);
            }

            return result;
        }
    }
}
