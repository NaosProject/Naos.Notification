// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouldNotSendOnAnyPreparedChannelEventTest.cs" company="Naos Project">
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
    public static partial class CouldNotSendOnAnyPreparedChannelEventTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CouldNotSendOnAnyPreparedChannelEventTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CouldNotSendOnAnyPreparedChannelEvent>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'attemptToSendNotificationResult' GetOutcome() is not AttemptToSendNotificationOutcome.CouldNotSendOnAnyPreparedChannel",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CouldNotSendOnAnyPreparedChannelEvent>();

                            var result = new CouldNotSendOnAnyPreparedChannelEvent(
                                referenceObject.Id,
                                referenceObject.TimestampUtc,
                                A.Dummy<AttemptToSendNotificationResult>().ThatIs(_ => _.GetOutcome() != AttemptToSendNotificationOutcome.CouldNotSendOnAnyPreparedChannel));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "attemptToSendNotificationOutcome", "CouldNotSendOnAnyPreparedChannel" },
                    });
        }
    }
}