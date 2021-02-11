// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationDummyFactoryTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Enum.Recipes;

    using Xunit;

    public static partial class NotificationDummyFactoryTest
    {
        [Fact]
        public static void A_Dummy_AttemptToSendNotificationResult___Should_return_objects_having_all_possible_AttemptToSendNotificationOutcome___When_called_many_times()
        {
            // Arrange
            IReadOnlyCollection<AttemptToSendNotificationOutcome> expected = EnumExtensions
                .GetDefinedEnumValues<AttemptToSendNotificationOutcome>()
                .Except(new[] { AttemptToSendNotificationOutcome.Unknown })
                .ToList();

            // Act
            var dummies = Some.ReadOnlyDummies<AttemptToSendNotificationResult>(1000);
            IReadOnlyCollection<AttemptToSendNotificationOutcome> actual = dummies.Select(_ => _.GetOutcome()).Distinct().ToList();

            // Assert
            expected.AsTest().Must().BeEqualTo(actual);
        }

        [Fact]
        public static void A_Dummy_GetAudienceResult___Should_return_objects_having_all_possible_GetAudienceOutcome___When_called_many_times()
        {
            // Arrange
            IReadOnlyCollection<GetAudienceOutcome> expected = EnumExtensions
                .GetDefinedEnumValues<GetAudienceOutcome>()
                .Except(new[] { GetAudienceOutcome.Unknown })
                .ToList();

            // Act
            var dummies = Some.ReadOnlyDummies<GetAudienceResult>(1000);
            IReadOnlyCollection<GetAudienceOutcome> actual = dummies.Select(_ => _.GetOutcome()).Distinct().ToList();

            // Assert
            expected.AsTest().Must().BeEqualTo(actual);
        }

        [Fact]
        public static void A_Dummy_GetDeliveryChannelConfigsResult___Should_return_objects_having_all_possible_GetDeliveryChannelConfigsOutcome___When_called_many_times()
        {
            // Arrange
            IReadOnlyCollection<GetDeliveryChannelConfigsOutcome> expected = EnumExtensions
                .GetDefinedEnumValues<GetDeliveryChannelConfigsOutcome>()
                .Except(new[] { GetDeliveryChannelConfigsOutcome.Unknown })
                .ToList();

            // Act
            var dummies = Some.ReadOnlyDummies<GetDeliveryChannelConfigsResult>(1000);
            IReadOnlyCollection<GetDeliveryChannelConfigsOutcome> actual = dummies.Select(_ => _.GetOutcome()).Distinct().ToList();

            // Assert
            expected.AsTest().Must().BeEqualTo(actual);
        }

        [Fact]
        public static void A_Dummy_PrepareToSendNotificationResult___Should_return_objects_having_all_possible_PrepareToSendNotificationOutcome___When_called_many_times()
        {
            // Arrange
            IReadOnlyCollection<PrepareToSendNotificationOutcome> expected = EnumExtensions
                .GetDefinedEnumValues<PrepareToSendNotificationOutcome>()
                .Except(new[] { PrepareToSendNotificationOutcome.Unknown })
                .ToList();

            // Act
            var dummies = Some.ReadOnlyDummies<PrepareToSendNotificationResult>(1000);
            IReadOnlyCollection<PrepareToSendNotificationOutcome> actual = dummies.Select(_ => _.GetOutcome()).Distinct().ToList();

            // Assert
            expected.AsTest().Must().BeEqualTo(actual);
        }

        [Fact]
        public static void A_Dummy_PrepareToSendOnChannelResult___Should_return_objects_having_all_possible_PrepareToSendOnChannelOutcome___When_called_many_times()
        {
            // Arrange
            IReadOnlyCollection<PrepareToSendOnChannelOutcome> expected = EnumExtensions
                .GetDefinedEnumValues<PrepareToSendOnChannelOutcome>()
                .Except(new[] { PrepareToSendOnChannelOutcome.Unknown })
                .ToList();

            // Act
            var dummies = Some.ReadOnlyDummies<PrepareToSendOnChannelResult>(1000);
            IReadOnlyCollection<PrepareToSendOnChannelOutcome> actual = dummies.Select(_ => _.GetOutcome()).Distinct().ToList();

            // Assert
            expected.AsTest().Must().BeEqualTo(actual);
        }
    }
}