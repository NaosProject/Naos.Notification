// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouldNotGetOrUseAudienceEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Could not get the audience for the notification or failures prevented the audience from being used.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CouldNotGetOrUseAudienceEvent : NotificationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CouldNotGetOrUseAudienceEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        public CouldNotGetOrUseAudienceEvent(
            long id,
            DateTime timestampUtc,
            GetAudienceResult getAudienceResult)
            : base(id, timestampUtc)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();
            var getAudienceOutcome = getAudienceResult.GetOutcome();
            new { getAudienceOutcome }.AsArg().Must().BeElementIn(new[] { GetAudienceOutcome.CouldNotGetAudienceAndNoFailuresReported, GetAudienceOutcome.CouldNotGetAudienceWithSomeFailuresReported, GetAudienceOutcome.DespiteGettingAudienceFailuresPreventUsingIt });

            this.GetAudienceResult = getAudienceResult;
        }

        /// <summary>
        /// Gets the result of executing a <see cref="GetAudienceOp"/>.
        /// </summary>
        public GetAudienceResult GetAudienceResult { get; private set; }
    }
}