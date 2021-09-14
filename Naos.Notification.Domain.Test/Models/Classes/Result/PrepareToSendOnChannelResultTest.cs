// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendOnChannelResultTest.cs" company="Naos Project">
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
    using OBeautifulCode.Type;
    using Xunit;
    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class PrepareToSendOnChannelResultTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static PrepareToSendOnChannelResultTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelOperationInstructions' is not null and contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelResult>();

                            var result = new PrepareToSendOnChannelResult(
                                                 new[]
                                                 {
                                                     A.Dummy<ChannelOperationInstruction>(),
                                                     null,
                                                     A.Dummy<ChannelOperationInstruction>(),
                                                 },
                                                 referenceObject.Failures,
                                                 referenceObject.FailureAction);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "channelOperationInstructions", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelOperationInstructions' contains two or more instructions with the same tracking code",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelResult>();

                            var channelOperationMonitoringInfo = A.Dummy<ChannelOperationMonitoringInfo>();

                            var channelOperationMonitoringInfo2 = A.Dummy<ChannelOperationMonitoringInfo>().DeepCloneWithChannelOperationTrackingCodeId(channelOperationMonitoringInfo.ChannelOperationTrackingCodeId);

                            var channelOperationInstructions = new[]
                            {
                                new ChannelOperationInstruction(A.Dummy<IOperation>(), channelOperationMonitoringInfo),
                                A.Dummy<ChannelOperationInstruction>(),
                                new ChannelOperationInstruction(A.Dummy<IOperation>(), channelOperationMonitoringInfo2),
                            };

                            var result = new PrepareToSendOnChannelResult(
                                channelOperationInstructions,
                                referenceObject.Failures,
                                referenceObject.FailureAction);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "channelOperationTrackingCodeIds", "contains two or more elements that are equal" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelOperationInstructions' contains two or more of the same operations",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelResult>().Whose(_ => (_.ChannelOperationInstructions != null) && _.ChannelOperationInstructions.Any());

                            var channelOperationInstructions = referenceObject
                                .ChannelOperationInstructions
                                .Concat(
                                    new[]
                                    {
                                        A.Dummy<ChannelOperationInstruction>().DeepCloneWithOperation(referenceObject.ChannelOperationInstructions.First().Operation),
                                    })
                                .ToList();

                            var result = new PrepareToSendOnChannelResult(
                                channelOperationInstructions,
                                referenceObject.Failures,
                                referenceObject.FailureAction);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "channelOperations", "contains two or more elements that are equal" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'failures' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelResult>();

                            var result = new PrepareToSendOnChannelResult(
                                                 referenceObject.ChannelOperationInstructions,
                                                 new[] { A.Dummy<IFailure>(), null, A.Dummy<IFailure>() },
                                                 referenceObject.FailureAction);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "failures", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendOnChannelResult>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'failureAction' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendOnChannelResult>();

                            var result = new PrepareToSendOnChannelResult(
                                referenceObject.ChannelOperationInstructions,
                                referenceObject.Failures,
                                PrepareToSendOnChannelFailureAction.Unknown);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "failureAction", "Unknown", },
                    });
        }
    }
}