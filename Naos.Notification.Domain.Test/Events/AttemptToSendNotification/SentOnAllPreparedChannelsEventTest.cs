// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SentOnAllPreparedChannelsEventTest.cs" company="Naos Project">
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
    public static partial class SentOnAllPreparedChannelsEventTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static SentOnAllPreparedChannelsEventTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SentOnAllPreparedChannelsEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'attemptToSendNotificationResult' GetOutcome() is not AttemptToSendNotificationOutcome.SentOnAllPreparedChannels",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SentOnAllPreparedChannelsEvent>();

                            var result = new SentOnAllPreparedChannelsEvent(
                                referenceObject.Id,
                                referenceObject.TimestampUtc,
                                A.Dummy<AttemptToSendNotificationResult>().ThatIs(_ => _.GetOutcome() != AttemptToSendNotificationOutcome.SentOnAllPreparedChannels));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "attemptToSendNotificationOutcome", "SentOnAllPreparedChannels" },
                    });
        }
    }
}