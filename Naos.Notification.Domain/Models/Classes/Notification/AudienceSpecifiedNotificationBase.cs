// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudienceSpecifiedNotificationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for notification to use when the system cannot determine the audience from the notification itself.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class AudienceSpecifiedNotificationBase : NotificationBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudienceSpecifiedNotificationBase"/> class.
        /// </summary>
        /// <param name="audience">Audience to send the notification to.</param>
        protected AudienceSpecifiedNotificationBase(
            IAudience audience)
        {
            new { audience }.AsArg().Must().NotBeNull();

            this.Audience = audience;
        }

        /// <summary>
        /// Gets the audience to send the notification to.
        /// </summary>
        public IAudience Audience { get; private set; }
    }
}