// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudienceOptedOutOfAllChannelsEventTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class AudienceOptedOutOfAllChannelsEventTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static AudienceOptedOutOfAllChannelsEventTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AudienceOptedOutOfAllChannelsEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'getAudienceResult' GetOutcome() is not any of GetAudienceOutcome.[GotAudienceWithNoFailuresReported|GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored]",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<AudienceOptedOutOfAllChannelsEvent>();

                            var result = new AudienceOptedOutOfAllChannelsEvent(
                                referenceObject.Id,
                                referenceObject.TimestampUtc,
                                A.Dummy<GetAudienceResult>().Whose(_ => (_.GetOutcome() != GetAudienceOutcome.GotAudienceWithNoFailuresReported) && (_.GetOutcome() != GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored)),
                                referenceObject.GetDeliveryChannelConfigsResult,
                                referenceObject.PrepareToSendNotificationResult);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "getAudienceOutcome", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AudienceOptedOutOfAllChannelsEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'getDeliveryChannelConfigsResult' GetOutcome() is not any of GetDeliveryChannelConfigsOutcome.[GotDeliveryChannelConfigsWithNoFailuresReported|GetAudienceOutcome.GotDeliveryChannelConfigsWithReportedFailuresIgnored]",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<AudienceOptedOutOfAllChannelsEvent>();

                            var result = new AudienceOptedOutOfAllChannelsEvent(
                                referenceObject.Id,
                                referenceObject.TimestampUtc,
                                referenceObject.GetAudienceResult,
                                A.Dummy<GetDeliveryChannelConfigsResult>().Whose(_ => (_.GetOutcome() != GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithNoFailuresReported) && (_.GetOutcome() != GetDeliveryChannelConfigsOutcome.GotDeliveryChannelConfigsWithReportedFailuresIgnored)),
                                referenceObject.PrepareToSendNotificationResult);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "getDeliveryChannelConfigsOutcome", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AudienceOptedOutOfAllChannelsEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'prepareToSendNotificationResult' GetOutcome() is is not PrepareToSendNotificationOutcome.AudienceOptedOutOfAllChannels",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<AudienceOptedOutOfAllChannelsEvent>();

                            var result = new AudienceOptedOutOfAllChannelsEvent(
                                referenceObject.Id,
                                referenceObject.TimestampUtc,
                                referenceObject.GetAudienceResult,
                                referenceObject.GetDeliveryChannelConfigsResult,
                                A.Dummy<PrepareToSendNotificationResult>().Whose(_ => _.GetOutcome() != PrepareToSendNotificationOutcome.AudienceOptedOutOfAllChannels));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "prepareToSendNotificationOutcome" },
                    });
        }
    }
}