// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouldNotSendOnAnyPreparedChannelEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The notification failed to be sent on any channel it was prepared to be sent on.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CouldNotSendOnAnyPreparedChannelEvent : AttemptToSendNotificationBaseEvent, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CouldNotSendOnAnyPreparedChannelEvent"/> class.
        /// </summary>
        /// <param name="id">The notification tracking code identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="attemptToSendNotificationResult">The result of attempting to send the notification.</param>
        public CouldNotSendOnAnyPreparedChannelEvent(
            long id,
            DateTime timestampUtc,
            AttemptToSendNotificationResult attemptToSendNotificationResult)
            : base(id, timestampUtc, attemptToSendNotificationResult)
        {
            var attemptToSendNotificationOutcome = attemptToSendNotificationResult.GetOutcome();

            new { attemptToSendNotificationOutcome }.AsArg().Must().BeEqualTo(AttemptToSendNotificationOutcome.CouldNotSendOnAnyPreparedChannel);
        }
    }
}
