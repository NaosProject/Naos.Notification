// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendOnChannelOpTest.cs" company="Naos Project">
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
    public static partial class PrepareToSendOnChannelOpTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static PrepareToSendOnChannelOpTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelOp>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'notification' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelOp>();

                            var result = new PrepareToSendOnChannelOp(
                                                 null,
                                                 referenceObject.Audience,
                                                 referenceObject.DeliveryChannel,
                                                 referenceObject.InheritableTags);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "notification", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelOp>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'audience' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelOp>();

                            var result = new PrepareToSendOnChannelOp(
                                                 referenceObject.Notification,
                                                 null,
                                                 referenceObject.DeliveryChannel,
                                                 referenceObject.InheritableTags);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "audience", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelOp>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'deliveryChannel' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelOp>();

                            var result = new PrepareToSendOnChannelOp(
                                                 referenceObject.Notification,
                                                 referenceObject.Audience,
                                                 null,
                                                 referenceObject.InheritableTags);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "deliveryChannel", },
                    });
        }
    }
}