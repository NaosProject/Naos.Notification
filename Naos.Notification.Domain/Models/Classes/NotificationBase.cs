// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="INotification"/>.
    /// </summary>
    public abstract partial class NotificationBase : INotification, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationBase"/> class.
        /// </summary>
        /// <param name="audience">OPTIONAL audience to send the notification to.  DEFAULT is null.  Should be set if the system cannot determine the audience from the notification itself.</param>
        protected NotificationBase(
            IAudience audience = null)
        {
            this.Audience = audience;
        }

        /// <summary>
        /// Gets the audience to send the notification to.
        /// </summary>
        public IAudience Audience { get; private set; }
    }
}