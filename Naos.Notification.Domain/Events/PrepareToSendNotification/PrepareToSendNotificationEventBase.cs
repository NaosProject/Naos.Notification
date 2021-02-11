﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendNotificationEventBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for events that categorize a <see cref="PrepareToSendNotificationResult"/> by <see cref="PrepareToSendNotificationOutcome"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class PrepareToSendNotificationEventBase : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrepareToSendNotificationEventBase"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        /// <param name="getDeliveryChannelConfigsResult">The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <param name="prepareToSendNotificationResult">The result of preparing to send the notification on all configured channels.</param>
        protected PrepareToSendNotificationEventBase(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult,
            GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult,
            PrepareToSendNotificationResult prepareToSendNotificationResult)
            : base(id, timestampUtc)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();
            var getAudienceOutcome = getAudienceResult.GetOutcome();
            new { getAudienceOutcome }.AsArg().Must().NotBeEqualTo(GetAudienceOutcome.CouldNotGetAudienceAndNoFailuresReported).And().NotBeEqualTo(GetAudienceOutcome.CouldNotGetAudienceWithSomeFailuresReported).And().NotBeEqualTo(GetAudienceOutcome.DespiteGettingAudienceFailuresPreventUsingIt);

            new { getDeliveryChannelConfigsResult }.AsArg().Must().NotBeNull();
            var getDeliveryChannelConfigsOutcome = getDeliveryChannelConfigsResult.GetOutcome();
            new { getDeliveryChannelConfigsOutcome }.AsArg().Must().NotBeEqualTo(GetDeliveryChannelConfigsOutcome.CouldNotGetDeliveryChannelConfigsAndNoFailuresReported).And().NotBeEqualTo(GetDeliveryChannelConfigsOutcome.CouldNotGetDeliveryChannelConfigsWithSomeFailuresReported).And().NotBeEqualTo(GetDeliveryChannelConfigsOutcome.DespiteGettingDeliveryChannelConfigsFailuresPreventUsingThem);

            new { prepareToSendNotificationResult }.AsArg().Must().NotBeNull();

            this.GetAudienceResult = getAudienceResult;
            this.GetDeliveryChannelConfigsResult = getDeliveryChannelConfigsResult;
            this.PrepareToSendNotificationResult = prepareToSendNotificationResult;
        }

        /// <summary>
        /// Gets the result of executing a <see cref="GetAudienceOp"/>.
        /// </summary>
        public GetAudienceResult GetAudienceResult { get; private set; }

        /// <summary>
        /// Gets the result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.
        /// </summary>
        public GetDeliveryChannelConfigsResult GetDeliveryChannelConfigsResult { get; private set; }

        /// <summary>
        /// Gets the result of preparing to send the notification on all configured channels.
        /// </summary>
        public PrepareToSendNotificationResult PrepareToSendNotificationResult { get; private set; }
    }
}