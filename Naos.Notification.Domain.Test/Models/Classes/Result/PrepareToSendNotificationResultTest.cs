// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrepareToSendNotificationResultTest.cs" company="Naos Project">
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
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class PrepareToSendNotificationResultTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static PrepareToSendNotificationResultTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'channelToPrepareToSendOnChannelResultMap' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>();

                            var result = new PrepareToSendNotificationResult(
                                                 null,
                                                 referenceObject.CannotPrepareToSendOnChannelAction,
                                                 referenceObject.ChannelsToSendOn);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "channelToPrepareToSendOnChannelResultMap", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToPrepareToSendOnChannelResultMap' contains a key-value pair with a null value scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>().Whose(_ => _.ChannelToPrepareToSendOnChannelResultMap.Any());

                            var dictionaryWithNullValue = referenceObject.ChannelToPrepareToSendOnChannelResultMap.ToDictionary(_ => _.Key, _ => _.Value);

                            var randomKey = dictionaryWithNullValue.Keys.ElementAt(ThreadSafeRandom.Next(0, dictionaryWithNullValue.Count));

                            dictionaryWithNullValue[randomKey] = null;

                            var result = new PrepareToSendNotificationResult(
                                                 dictionaryWithNullValue,
                                                 referenceObject.CannotPrepareToSendOnChannelAction,
                                                 referenceObject.ChannelsToSendOn);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "channelToPrepareToSendOnChannelResultMap", "contains at least one key-value pair with a null value", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelToPrepareToSendOnChannelResultMap' values are not distinct",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>();

                            var prepareToSendOnChannelResult = A.Dummy<PrepareToSendOnChannelResult>();

                            var channelToPrepareToSendOnChannelResultMap = new Dictionary<IDeliveryChannel, PrepareToSendOnChannelResult>
                            {
                                {
                                    new SlackDeliveryChannel(),
                                    prepareToSendOnChannelResult
                                },
                                {
                                    new EmailDeliveryChannel(),
                                    prepareToSendOnChannelResult
                                },
                            };

                            var result = new PrepareToSendNotificationResult(
                                channelToPrepareToSendOnChannelResultMap,
                                referenceObject.CannotPrepareToSendOnChannelAction,
                                null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "prepareToSendOnChannelResult", "contains two or more elements that are equal" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'cannotPrepareToSendOnChannelAction' is CannotPrepareToSendOnChannelAction.Unknown.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>();

                            var result = new PrepareToSendNotificationResult(
                                referenceObject.ChannelToPrepareToSendOnChannelResultMap,
                                CannotPrepareToSendOnChannelAction.Unknown,
                                referenceObject.ChannelsToSendOn);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "cannotPrepareToSendOnChannelAction", "Unknown" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'channelsToSendOn' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>();

                            var result = new PrepareToSendNotificationResult(
                                                 referenceObject.ChannelToPrepareToSendOnChannelResultMap,
                                                 referenceObject.CannotPrepareToSendOnChannelAction,
                                                 null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "channelsToSendOn", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelsToSendOn' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>();

                            var result = new PrepareToSendNotificationResult(
                                                 referenceObject.ChannelToPrepareToSendOnChannelResultMap,
                                                 referenceObject.CannotPrepareToSendOnChannelAction,
                                                 new IDeliveryChannel[0].Concat(referenceObject.ChannelsToSendOn).Concat(new IDeliveryChannel[] { null }).Concat(referenceObject.ChannelsToSendOn).ToList());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "channelsToSendOn", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'channelsToSendOn' has a channel that's not a key in 'channelToPrepareToSendOnChannelResultMap'",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>();

                            var result = new PrepareToSendNotificationResult(
                                new Dictionary<IDeliveryChannel, PrepareToSendOnChannelResult>
                                {
                                    { new SlackDeliveryChannel(), A.Dummy<PrepareToSendOnChannelResult>() },
                                },
                                referenceObject.CannotPrepareToSendOnChannelAction,
                                new[] { new EmailDeliveryChannel() });

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "channelToPrepareToSendOnChannelResultMap", "does not contain the key to search for", },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "DeepCloneWithChannelToPrepareToSendOnChannelResultMap should deep clone object and replace ChannelToPrepareToSendOnChannelResultMap with the provided channelToPrepareToSendOnChannelResultMap",
                        WithPropertyName = "ChannelToPrepareToSendOnChannelResultMap",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<PrepareToSendNotificationResult>();

                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>().ThatIs(_ => (!systemUnderTest.ChannelToPrepareToSendOnChannelResultMap.IsEqualTo(_.ChannelToPrepareToSendOnChannelResultMap)) && systemUnderTest.ChannelsToSendOn.IsEqualTo(_.ChannelsToSendOn));

                            var result = new SystemUnderTestDeepCloneWithValue<PrepareToSendNotificationResult>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ChannelToPrepareToSendOnChannelResultMap,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "DeepCloneWithCannotPrepareToSendOnChannelAction should deep clone object and replace CannotPrepareToSendOnChannelAction with the provided cannotPrepareToSendOnChannelAction",
                        WithPropertyName = "CannotPrepareToSendOnChannelAction",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<PrepareToSendNotificationResult>();

                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>().ThatIs(_ => !systemUnderTest.CannotPrepareToSendOnChannelAction.IsEqualTo(_.CannotPrepareToSendOnChannelAction));

                            var result = new SystemUnderTestDeepCloneWithValue<PrepareToSendNotificationResult>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.CannotPrepareToSendOnChannelAction,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "DeepCloneWithChannelsToSendOn should deep clone object and replace ChannelsToSendOn with the provided channelsToSendOn",
                        WithPropertyName = "ChannelsToSendOn",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<PrepareToSendNotificationResult>().Whose(_ => _.ChannelToPrepareToSendOnChannelResultMap.Any());

                            var referenceObject = A.Dummy<PrepareToSendNotificationResult>().ThatIs(_ => (!systemUnderTest.ChannelsToSendOn.IsEqualTo(_.ChannelsToSendOn)) && _.ChannelsToSendOn.All(c => systemUnderTest.ChannelToPrepareToSendOnChannelResultMap.ContainsKey(c)));

                            var result = new SystemUnderTestDeepCloneWithValue<PrepareToSendNotificationResult>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ChannelsToSendOn,
                            };

                            return result;
                        },
                    });

            var referenceObjectForEquatableTestScenarios = A.Dummy<PrepareToSendNotificationResult>().Whose(_ => _.ChannelToPrepareToSendOnChannelResultMap.Any());

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<PrepareToSendNotificationResult>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new PrepareToSendNotificationResult[]
                        {
                            new PrepareToSendNotificationResult(
                                referenceObjectForEquatableTestScenarios.ChannelToPrepareToSendOnChannelResultMap,
                                referenceObjectForEquatableTestScenarios.CannotPrepareToSendOnChannelAction,
                                referenceObjectForEquatableTestScenarios.ChannelsToSendOn),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new PrepareToSendNotificationResult[]
                        {
                            new PrepareToSendNotificationResult(
                                    A.Dummy<PrepareToSendNotificationResult>().Whose(_ => (!_.ChannelToPrepareToSendOnChannelResultMap.IsEqualTo(referenceObjectForEquatableTestScenarios.ChannelToPrepareToSendOnChannelResultMap)) && referenceObjectForEquatableTestScenarios.ChannelsToSendOn.All(c => _.ChannelToPrepareToSendOnChannelResultMap.ContainsKey(c))).ChannelToPrepareToSendOnChannelResultMap,
                                    referenceObjectForEquatableTestScenarios.CannotPrepareToSendOnChannelAction,
                                    referenceObjectForEquatableTestScenarios.ChannelsToSendOn),
                            new PrepareToSendNotificationResult(
                                    referenceObjectForEquatableTestScenarios.ChannelToPrepareToSendOnChannelResultMap,
                                    A.Dummy<PrepareToSendNotificationResult>().Whose(_ => !_.CannotPrepareToSendOnChannelAction.IsEqualTo(referenceObjectForEquatableTestScenarios.CannotPrepareToSendOnChannelAction)).CannotPrepareToSendOnChannelAction,
                                    referenceObjectForEquatableTestScenarios.ChannelsToSendOn),
                            new PrepareToSendNotificationResult(
                                    referenceObjectForEquatableTestScenarios.ChannelToPrepareToSendOnChannelResultMap,
                                    referenceObjectForEquatableTestScenarios.CannotPrepareToSendOnChannelAction,
                                    A.Dummy<PrepareToSendNotificationResult>().Whose(_ => (!_.ChannelsToSendOn.IsEqualTo(referenceObjectForEquatableTestScenarios.ChannelsToSendOn)) && _.ChannelsToSendOn.All(c => referenceObjectForEquatableTestScenarios.ChannelToPrepareToSendOnChannelResultMap.ContainsKey(c))).ChannelsToSendOn),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                        },
                    });
        }
    }
}