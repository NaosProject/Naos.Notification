// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationTrackingCode.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Globalization;

    using OBeautifulCode.Type;

    /// <summary>
    /// Tracking code used to retrieve the status and other information related to an <see cref="INotification"/>.
    /// </summary>
    public partial class NotificationTrackingCode : IModelViaCodeGen, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationTrackingCode"/> class.
        /// </summary>
        /// <param name="id">The tracking code identifier.</param>
        public NotificationTrackingCode(
            long id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the tracking code identifier.
        /// </summary>
        public long Id { get; private set; }

        /// <inheritdoc cref="IDeclareToStringMethod.ToString" />
        public override string ToString()
        {
            var result = this.Id.ToString(CultureInfo.InvariantCulture);

            return result;
        }
    }
}