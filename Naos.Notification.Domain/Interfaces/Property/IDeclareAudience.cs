// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeclareAudience.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    /// <summary>
    /// Declares an audience.
    /// </summary>
    public interface IDeclareAudience
    {
        /// <summary>
        /// Gets the audience.
        /// </summary>
        IAudience Audience { get; }
    }
}
