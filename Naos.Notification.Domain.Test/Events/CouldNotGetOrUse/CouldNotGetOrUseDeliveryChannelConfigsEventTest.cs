// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouldNotGetOrUseDeliveryChannelConfigsEventTest.cs" company="Naos Project">
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
    public static partial class CouldNotGetOrUseDeliveryChannelConfigsEventTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CouldNotGetOrUseDeliveryChannelConfigsEventTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CouldNotGetOrUseDeliveryChannelConfigsEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'getAudienceResult' GetOutcome() is not any of GetAudienceOutcome.[GotAudienceWithNoFailuresReported|GotAudienceWithReportedFailuresIgnored]",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CouldNotGetOrUseDeliveryChannelConfigsEvent>();

                            var result = new CouldNotGetOrUseDeliveryChannelConfigsEvent(
                                                 referenceObject.Id,
                                                 referenceObject.TimestampUtc,
                                                 A.Dummy<GetAudienceResult>().Whose(_ => (_.GetOutcome() != GetAudienceOutcome.GotAudienceWithNoFailuresReported) && (_.GetOutcome() != GetAudienceOutcome.GotAudienceWithReportedFailuresIgnored)),
                                                 referenceObject.GetDeliveryChannelConfigsResult);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "getAudienceOutcome" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CouldNotGetOrUseDeliveryChannelConfigsEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'getDeliveryChannelConfigsResult' GetOutcome() is not any of GetDeliveryChannelConfigsOutcome.[CouldNotGetDeliveryChannelConfigsAndNoFailuresReported|CouldNotGetDeliveryChannelConfigsWithSomeFailuresReported|DespiteGettingDeliveryChannelConfigsFailuresPreventUsingThem]",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CouldNotGetOrUseDeliveryChannelConfigsEvent>();

                            var result = new CouldNotGetOrUseDeliveryChannelConfigsEvent(
                                                 referenceObject.Id,
                                                 referenceObject.TimestampUtc,
                                                 referenceObject.GetAudienceResult,
                                                 A.Dummy<GetDeliveryChannelConfigsResult>().Whose(_ => !new[] { GetDeliveryChannelConfigsOutcome.CouldNotGetDeliveryChannelConfigsAndNoFailuresReported, GetDeliveryChannelConfigsOutcome.CouldNotGetDeliveryChannelConfigsWithSomeFailuresReported, GetDeliveryChannelConfigsOutcome.DespiteGettingDeliveryChannelConfigsFailuresPreventUsingThem }.Contains(_.GetOutcome())));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "getDeliveryChannelConfigsOutcome" },
                    });
        }
    }
}