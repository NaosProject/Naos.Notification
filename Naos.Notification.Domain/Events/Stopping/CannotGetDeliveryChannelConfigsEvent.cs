﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CannotGetDeliveryChannelConfigsEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Cannot get the delivery channels for the notification and audience.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CannotGetDeliveryChannelConfigsEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CannotGetDeliveryChannelConfigsEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsResult">The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        public CannotGetDeliveryChannelConfigsEvent(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult)
            : base(id, timestampUtc)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();
            new { getAudienceResult.Audience }.AsArg().Must().NotBeNull();
            new { getDeliveryChannelConfigsResult }.AsArg().Must().NotBeNull();

            this.GetAudienceResult = getAudienceResult;
            this.GetDeliveryChannelConfigsResult = getDeliveryChannelConfigsResult;
        }

        /// <summary>
        /// Gets the result of executing a <see cref="GetAudienceOp"/>.
        /// </summary>
        public GetAudienceResult GetAudienceResult { get; private set; }

        /// <summary>
        /// Gets the result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.
        /// </summary>
        public GetDeliveryChannelConfigsResult GetDeliveryChannelConfigsResult { get; private set; }
    }
}