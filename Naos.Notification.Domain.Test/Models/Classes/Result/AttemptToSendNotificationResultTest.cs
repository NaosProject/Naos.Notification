// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttemptToSendNotificationResultTest.cs" company="Naos Project">
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
    public static partial class AttemptToSendNotificationResultTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static AttemptToSendNotificationResultTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AttemptToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToOperationsOutcomeInfoMap' contains a value that is empty",
                        ConstructionFunc = () =>
                        {
                            var channelToOperationsOutcomeInfoMap =
                                new Dictionary<IDeliveryChannel, IReadOnlyCollection<ChannelOperationOutcomeInfo>>
                                {
                                    {  new SlackDeliveryChannel(), new ChannelOperationOutcomeInfo[0] },
                                };

                            var result = new AttemptToSendNotificationResult(
                                channelToOperationsOutcomeInfoMap);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "perChannelOperationsOutcomeInfo", "empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AttemptToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToOperationsOutcomeInfoMap' contains a value that contains a null value",
                        ConstructionFunc = () =>
                        {
                            var channelToOperationsOutcomeInfoMap =
                                new Dictionary<IDeliveryChannel, IReadOnlyCollection<ChannelOperationOutcomeInfo>>
                                {
                                    {  new SlackDeliveryChannel(), new ChannelOperationOutcomeInfo[] { A.Dummy<ChannelOperationOutcomeInfo>(), null } },
                                };

                            var result = new AttemptToSendNotificationResult(
                                channelToOperationsOutcomeInfoMap);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "perChannelOperationsOutcomeInfo", "null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AttemptToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToOperationsOutcomeInfoMap' contains a value having duplicate tracking code ids",
                        ConstructionFunc = () =>
                        {
                            var channelOperationOutcomeInfo = A.Dummy<ChannelOperationOutcomeInfo>();

                            var channelOperationOutcomeInfo2 = A.Dummy<ChannelOperationOutcomeInfo>().DeepCloneWithChannelOperationTrackingCodeId(channelOperationOutcomeInfo.ChannelOperationTrackingCodeId);

                            var channelToOperationsOutcomeInfoMap =
                                new Dictionary<IDeliveryChannel, IReadOnlyCollection<ChannelOperationOutcomeInfo>>
                                {
                                    {
                                        new SlackDeliveryChannel(),
                                        new[]
                                        {
                                            channelOperationOutcomeInfo,
                                            channelOperationOutcomeInfo2,
                                        }
                                    },
                                };

                            var result = new AttemptToSendNotificationResult(
                                channelToOperationsOutcomeInfoMap);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "perChannelOperationTrackingCodeIds", "two or more elements that are equal", },
                    });
        }
    }
}