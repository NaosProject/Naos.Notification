// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetDeliveryChannelConfigsResultTest.cs" company="Naos Project">
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
    public static partial class GetDeliveryChannelConfigsResultTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static GetDeliveryChannelConfigsResultTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GetDeliveryChannelConfigsResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'configs' is not null and contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<GetDeliveryChannelConfigsResult>();

                            var result = new GetDeliveryChannelConfigsResult(
                                                 new[] { A.Dummy<DeliveryChannelConfig>(), null, A.Dummy<DeliveryChannelConfig>() },
                                                 referenceObject.Failures,
                                                 referenceObject.FailureAction);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "configs", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GetDeliveryChannelConfigsResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'configs' contains duplicate delivery channels",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<GetDeliveryChannelConfigsResult>();

                            var result = new GetDeliveryChannelConfigsResult(
                                new[]
                                {
                                    new DeliveryChannelConfig(new EmailDeliveryChannel(), DeliveryChannelAction.SendOnChannel),
                                    new DeliveryChannelConfig(new SlackDeliveryChannel(), DeliveryChannelAction.AudienceOptedOut),
                                    new DeliveryChannelConfig(new EmailDeliveryChannel(), DeliveryChannelAction.SendOnChannel),
                                },
                                referenceObject.Failures,
                                referenceObject.FailureAction);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "deliveryChannels", "contains two or more elements that are equal", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GetDeliveryChannelConfigsResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'failures' is not null and contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<GetDeliveryChannelConfigsResult>();

                            var result = new GetDeliveryChannelConfigsResult(
                                                 referenceObject.Configs,
                                                 new[] { A.Dummy<IFailure>(), null, A.Dummy<IFailure>() },
                                                 referenceObject.FailureAction);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "failures", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<GetDeliveryChannelConfigsResult>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'failureAction' is FailureAction.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<GetDeliveryChannelConfigsResult>();

                            var result = new GetDeliveryChannelConfigsResult(
                                referenceObject.Configs,
                                referenceObject.Failures,
                                FailureAction.Unknown);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "failureAction", "Unknown", },
                    });
        }
    }
}