// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessSendNotificationSagaOpTest.cs" company="Naos Project">
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
    public static partial class ProcessSendNotificationSagaOpTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ProcessSendNotificationSagaOpTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ProcessSendNotificationSagaOp>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToOperationsMonitoringInfoMap' has a value that is empty",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ProcessSendNotificationSagaOp>();

                            var channelToOperationsMonitoringInfoMap =
                                new Dictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationMonitoringInfo>>
                                {
                                    {
                                        new SlackDeliveryChannel(),
                                        new ChannelOperationMonitoringInfo[0]
                                    },
                                };

                            var result = new ProcessSendNotificationSagaOp(
                                referenceObject.NotificationTrackingCodeId,
                                channelToOperationsMonitoringInfoMap);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "perChannelOperationsMonitoringInfo", "empty enumerable" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ProcessSendNotificationSagaOp>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToOperationsMonitoringInfoMap' has a value that contains a null element",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ProcessSendNotificationSagaOp>();

                            var channelToOperationsMonitoringInfoMap =
                                new Dictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationMonitoringInfo>>
                                {
                                    {
                                        new SlackDeliveryChannel(),
                                        new[] { A.Dummy<ChannelOperationMonitoringInfo>(), null }
                                    },
                                };

                            var result = new ProcessSendNotificationSagaOp(
                                referenceObject.NotificationTrackingCodeId,
                                channelToOperationsMonitoringInfoMap);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "perChannelOperationsMonitoringInfo", "null element" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ProcessSendNotificationSagaOp>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToOperationsMonitoringInfoMap' has a value that contains two or more elements with the same tracking code",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ProcessSendNotificationSagaOp>();

                            var channelMonitoringInfo = A.Dummy<ChannelOperationMonitoringInfo>();

                            var channelMonitoringInfo2 = A.Dummy<ChannelOperationMonitoringInfo>().DeepCloneWithChannelOperationTrackingCodeId(channelMonitoringInfo.ChannelOperationTrackingCodeId);

                            var channelToOperationsMonitoringInfoMap =
                                new Dictionary<IDeliveryChannel, IReadOnlyList<ChannelOperationMonitoringInfo>>
                                {
                                    {
                                        new SlackDeliveryChannel(),
                                        new[]
                                        {
                                            channelMonitoringInfo,
                                            A.Dummy<ChannelOperationMonitoringInfo>(),
                                            channelMonitoringInfo2,
                                        }
                                    },
                                };

                            var result = new ProcessSendNotificationSagaOp(
                                referenceObject.NotificationTrackingCodeId,
                                channelToOperationsMonitoringInfoMap);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "channelOperationTrackingCodeIds", "two or more elements that are equal" },
                    });
        }
    }
}