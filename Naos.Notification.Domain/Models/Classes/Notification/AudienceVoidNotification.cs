// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudienceVoidNotification.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// No notification with an audience.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class AudienceVoidNotification : AudienceSpecifiedNotificationBase, IDeclareAudience, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudienceVoidNotification"/> class.
        /// </summary>
        /// <param name="audience">The audience.</param>
        public AudienceVoidNotification(
            IAudience audience)
            : base(audience)
        {
        }
    }
}