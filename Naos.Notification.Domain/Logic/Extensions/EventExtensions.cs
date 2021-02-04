// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventExtensions.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Linq;

    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Extension methods on Events.
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Summarize a <see cref="AttemptedToSendNotificationEvent"/> as a <see cref="AttemptToSendNotificationOutcome" />.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <returns>
        /// A <see cref="AttemptToSendNotificationOutcome"/> summarized in a <see cref="AttemptToSendNotificationOutcome"/>.
        /// </returns>
        private static AttemptToSendNotificationOutcome GetAttemptToSendNotificationOutcome(
            AttemptedToSendNotificationEvent @event)
        {
            new { @event }.AsArg().Must().NotBeNull();

            AttemptToSendNotificationOutcome result;

            if (@event.FailedChannels.Any())
            {
                if (@event.SucceededChannels.Any())
                {
                    if (@event.FailedChannels.Intersect(@event.SucceededChannels).Any())
                    {
                        result = AttemptToSendNotificationOutcome.MixedWithPartialChannelSends;
                    }
                    else
                    {
                        result = AttemptToSendNotificationOutcome.MixedWithFullChannelSends;
                    }
                }
                else
                {
                    result = AttemptToSendNotificationOutcome.FailedOnAllChannels;
                }
            }
            else
            {
                result = AttemptToSendNotificationOutcome.SucceededOnAllChannels;
            }

            return result;
        }
    }
}
