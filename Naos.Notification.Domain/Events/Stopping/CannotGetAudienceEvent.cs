// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CannotGetAudienceEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Cannot get the audience for a notification.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CannotGetAudienceEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CannotGetAudienceEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        public CannotGetAudienceEvent(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult)
            : base(id, timestampUtc)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();
            var getAudienceOutcome = getAudienceResult.GetOutcome();
            new { getAudienceOutcome }.AsArg().Must().NotBeEqualTo(GetAudienceOutcome.GotAudienceWithNoFailuresReported).And().NotBeEqualTo(GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored);

            this.GetAudienceResult = getAudienceResult;
        }

        /// <summary>
        /// Gets the result of executing a <see cref="GetAudienceOp"/>.
        /// </summary>
        public GetAudienceResult GetAudienceResult { get; private set; }
    }
}