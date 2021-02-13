// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouldNotGetOrUseAudienceEventTest.cs" company="Naos Project">
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
    public static partial class CouldNotGetOrUseAudienceEventTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CouldNotGetOrUseAudienceEventTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CouldNotGetOrUseAudienceEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'getAudienceResult' GetOutcome() is not any of GetAudienceOutcome.[CouldNotGetAudienceAndNoFailuresReported|CouldNotGetAudienceWithSomeFailuresReported|DespiteGettingAudienceFailuresPreventUsingIt]",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CouldNotGetOrUseAudienceEvent>();

                            var result = new CouldNotGetOrUseAudienceEvent(
                                referenceObject.Id,
                                referenceObject.TimestampUtc,
                                A.Dummy<GetAudienceResult>().Whose(_ => !new[] { GetAudienceOutcome.CouldNotGetAudienceAndNoFailuresReported, GetAudienceOutcome.CouldNotGetAudienceWithSomeFailuresReported, GetAudienceOutcome.DespiteGettingAudienceFailuresPreventUsingIt }.Contains(_.GetOutcome())));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "getAudienceOutcome" },
                    });
        }
    }
}