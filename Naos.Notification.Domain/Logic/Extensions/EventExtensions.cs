// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventExtensions.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System;
    using System.Linq;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Collection.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on Events.
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Summarize a <see cref="GetAudienceResult"/> as a <see cref="GetAudienceOutcome" />.
        /// </summary>
        /// <param name="getAudienceResult">The result of executing a <see cref="GetAudienceOp"/>.</param>
        /// <returns>
        /// A <see cref="GetAudienceResult"/> summarized as <see cref="GetAudienceOutcome"/>.
        /// </returns>
        public static GetAudienceOutcome GetOutcome(
            this GetAudienceResult getAudienceResult)
        {
            new { getAudienceResult }.AsArg().Must().NotBeNull();

            GetAudienceOutcome result;

            if (getAudienceResult.Audience == null)
            {
                if ((getAudienceResult.Failures == null) || (!getAudienceResult.Failures.Any()))
                {
                    result = GetAudienceOutcome.CouldNotGetAudienceAndNoFailuresReported;
                }
                else
                {
                    result = GetAudienceOutcome.CouldNotGetAudienceWithSomeFailuresReported;
                }
            }
            else
            {
                if ((getAudienceResult.Failures == null) || (!getAudienceResult.Failures.Any()))
                {
                    result = GetAudienceOutcome.GotAudienceWithNoFailuresReported;
                }
                else
                {
                    if (getAudienceResult.FailureAction == FailureAction.Stop)
                    {
                        result = GetAudienceOutcome.DespiteGettingAudienceFailuresPreventUsingIt;
                    }
                    else if (getAudienceResult.FailureAction == FailureAction.IgnoreAndProceedIfPossibleOtherwiseStop)
                    {
                        result = GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(FailureAction)} is not supported: {getAudienceResult.FailureAction}."));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Summarize a <see cref="GetDeliveryChannelConfigsResult"/> as a <see cref="GetDeliveryChannelConfigsOutcome" />.
        /// </summary>
        /// <param name="getDeliveryChannelConfigsResult">The result of executing a <see cref="GetDeliveryChannelConfigsOp"/>.</param>
        /// <returns>
        /// A <see cref="GetDeliveryChannelConfigsResult"/> summarized as <see cref="GetDeliveryChannelConfigsOutcome"/>.
        /// </returns>
        public static GetDeliveryChannelConfigsOutcome GetOutcome(
            this GetDeliveryChannelConfigsResult getDeliveryChannelConfigsResult)
        {
            new { getDeliveryChannelConfigsResult }.AsArg().Must().NotBeNull();

            GetDeliveryChannelConfigsOutcome result;

            if ((getDeliveryChannelConfigsResult.Configs == null) || (!getDeliveryChannelConfigsResult.Configs.Any()))
            {
                if ((getDeliveryChannelConfigsResult.Failures == null) || (!getDeliveryChannelConfigsResult.Failures.Any()))
                {
                    result = GetDeliveryChannelConfigsOutcome.CouldNotGetDeliveryChannelConfigsAndNoFailuresReported;
                }
                else
                {
                    result = GetDeliveryChannelConfigsOutcome.CouldNotGetDeliveryChannelConfigsWithSomeFailuresReported;
                }
            }
            else
            {
                if ((getDeliveryChannelConfigsResult.Failures == null) || (!getDeliveryChannelConfigsResult.Failures.Any()))
                {
                    result = GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithNoFailuresReported;
                }
                else
                {
                    if (getDeliveryChannelConfigsResult.FailureAction == FailureAction.Stop)
                    {
                        result = GetDeliveryChannelConfigsOutcome.DespiteGettingDeliveryChannelConfigsFailuresPreventUsingThem;
                    }
                    else if (getDeliveryChannelConfigsResult.FailureAction == FailureAction.IgnoreAndProceedIfPossibleOtherwiseStop)
                    {
                        result = GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithReportedFailuresIgnored;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(FailureAction)} is not supported: {getDeliveryChannelConfigsResult.FailureAction}."));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Summarize a <see cref="PrepareToSendOnChannelResult"/> as a <see cref="PrepareToSendOnChannelOutcome" />.
        /// </summary>
        /// <param name="prepareToSendOnChannelResult">The result of executing a <see cref="PrepareToSendOnChannelOp"/>.</param>
        /// <returns>
        /// A <see cref="PrepareToSendOnChannelResult"/> summarized as <see cref="PrepareToSendOnChannelOutcome"/>.
        /// </returns>
        public static PrepareToSendOnChannelOutcome GetOutcome(
            this PrepareToSendOnChannelResult prepareToSendOnChannelResult)
        {
            new { prepareToSendOnChannelResult }.AsArg().Must().NotBeNull();

            PrepareToSendOnChannelOutcome result;

            if ((prepareToSendOnChannelResult.ChannelOperationInstructions == null) || (!prepareToSendOnChannelResult.ChannelOperationInstructions.Any()))
            {
                if ((prepareToSendOnChannelResult.Failures == null) || (!prepareToSendOnChannelResult.Failures.Any()))
                {
                    result = PrepareToSendOnChannelOutcome.CouldNotPrepareToSendOnChannelAndNoFailuresReported;
                }
                else
                {
                    result = PrepareToSendOnChannelOutcome.CouldNotPrepareToSendOnChannelWithSomeFailuresReported;
                }
            }
            else
            {
                if ((prepareToSendOnChannelResult.Failures == null) || (!prepareToSendOnChannelResult.Failures.Any()))
                {
                    result = PrepareToSendOnChannelOutcome.PreparedToSendOnChannelWithNoFailuresReported;
                }
                else
                {
                    if (prepareToSendOnChannelResult.FailureAction == PrepareToSendOnChannelFailureAction.DoNotSendOnChannel)
                    {
                        result = PrepareToSendOnChannelOutcome.DespitePreparingToSendOnChannelFailuresPreventUsingIt;
                    }
                    else if (prepareToSendOnChannelResult.FailureAction == PrepareToSendOnChannelFailureAction.IgnoreAndProceedIfPossibleOtherwiseDoNotSendOnChannel)
                    {
                        result = PrepareToSendOnChannelOutcome.PreparedToSendOnChannelWithReportedFailuresIgnored;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(PrepareToSendOnChannelFailureAction)} is not supported: {prepareToSendOnChannelResult.FailureAction}."));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Summarize a <see cref="PrepareToSendNotificationResult"/> as a <see cref="PrepareToSendNotificationOutcome" />.
        /// </summary>
        /// <param name="prepareToSendNotificationResult">The result of preparing to send a notification on all configured channels.</param>
        /// <returns>
        /// A <see cref="PrepareToSendNotificationResult"/> summarized as <see cref="PrepareToSendNotificationOutcome"/>.
        /// </returns>
        public static PrepareToSendNotificationOutcome GetOutcome(
            this PrepareToSendNotificationResult prepareToSendNotificationResult)
        {
            new { prepareToSendNotificationResult }.AsArg().Must().NotBeNull();

            PrepareToSendNotificationOutcome result;

            var channelToPrepareToSendOnChannelResultMap = prepareToSendNotificationResult.ChannelToPrepareToSendOnChannelResultMap;
            var channelsToSendOn = prepareToSendNotificationResult.ChannelsToSendOn;
            var cannotPrepareToSendOnChannelAction = prepareToSendNotificationResult.CannotPrepareToSendOnChannelAction;

            if (!channelToPrepareToSendOnChannelResultMap.Any())
            {
                result = PrepareToSendNotificationOutcome.AudienceOptedOutOfAllChannels;
            }
            else if (!channelToPrepareToSendOnChannelResultMap.Keys.SymmetricDifference(channelsToSendOn).Any())
            {
                result = PrepareToSendNotificationOutcome.PreparedToSendOnAllChannels;
            }
            else if (channelsToSendOn.Any())
            {
                result = PrepareToSendNotificationOutcome.PreparedToSendOnSomeChannels;
            }
            else if (cannotPrepareToSendOnChannelAction == CannotPrepareToSendOnChannelAction.StopAndNotDoNotSendOnAnyChannel)
            {
                result = PrepareToSendNotificationOutcome.CouldNotPrepareToSendOnAnyChannelBecauseOneForcedAllToBeDiscarded;
            }
            else if (cannotPrepareToSendOnChannelAction == CannotPrepareToSendOnChannelAction.ContinueAndAttemptPreparingToSendOnNextChannel)
            {
                result = PrepareToSendNotificationOutcome.CouldNotPrepareToSendOnAnyChannelDespiteAttemptingAll;
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(CannotPrepareToSendOnChannelAction)} is not supported: {cannotPrepareToSendOnChannelAction}."));
            }

            return result;
        }

        /// <summary>
        /// Summarize a <see cref="AttemptedToSendNotificationEvent"/> as a <see cref="AttemptToSendNotificationOutcome" />.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <returns>
        /// A <see cref="AttemptedToSendNotificationEvent"/> summarized in a <see cref="AttemptToSendNotificationOutcome"/>.
        /// </returns>
        public static AttemptToSendNotificationOutcome GetOutcome(
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
