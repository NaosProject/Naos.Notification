// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationEventBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for all events.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class NotificationEventBase : EventBase<long>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEventBase"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        protected NotificationEventBase(
            long id,
            DateTime timestampUtc)
            : base(id, timestampUtc)
        {
        }
    }
}