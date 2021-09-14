// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegrationTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Protocol.Bot.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Naos.Database.Domain;
    using Naos.Database.Protocol.FileSystem;
    using Naos.Database.Serialization.Json;
    using Naos.Email.Domain;
    using Naos.Email.Protocol.Client;
    using Naos.Email.Serialization.Json;
    using Naos.Notification.Domain;
    using Naos.Notification.Protocol.Client;
    using Naos.Notification.Protocol.Email.Bot;
    using Naos.Notification.Protocol.Slack.Bot;
    using Naos.Notification.Serialization.Json;
    using Naos.Slack.Domain;
    using Naos.Slack.Protocol.Client;
    using Naos.Slack.Serialization.Json;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Cloning.Recipes;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Json;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using Xunit;

    public static class IntegrationTest
    {
        private const string StreamRootDirectoryPath = "DIRECTORY_PATH_HERE";

        private static readonly SmtpServerConnectionDefinition SmtpServerConnectionDefinition = null; // OBJECT NEEDED HERE

        private static readonly SlackAuthToken SlackAuthenticationToken = null; // AUTH TOKEN NEEDED HERE

        private static readonly INotification Notification = new IntegrationTestNotification
        {
            ScenarioBeingTested = "SCENARIO_BEING_TESTED_HERE",
        };

        private static readonly IAudience Audience = new IntegrationTestAudience
        {
            SenderEmailAddress = "SENDER_EMAIL_HERE",
            RecipientEmailAddress = "RECIPIENT_EMAIL_HERE",
            SlackChannelId = "SLACK_CHANNEL_ID_HERE",
        };

        [Fact(Skip = "For local testing only.")]
        public static async Task Send_a_notification()
        {
            var streams = await BuildStreamsAsync(StreamRootDirectoryPath);

            var trackingCode = await SendNotificationUsingClientAsync(streams);

            new { trackingCode }.AsTest().Must().NotBeNull();

            await ProcessNotificationsUsingBotAsync(streams);
        }

        private static async Task<NotificationTrackingCode> SendNotificationUsingClientAsync(
            Streams streams)
        {
            var buildExecuteSendNotificationEventTagsProtocol = new BuildExecuteSendNotificationEventTagsProtocol();

            var protocol = new SendNotificationProtocol(streams.ClientOperationStream, buildExecuteSendNotificationEventTagsProtocol);

            var sendNotificationOp = new SendNotificationOp(Notification);

            var result = await protocol.ExecuteAsync(sendNotificationOp);

            return result;
        }

        private static async Task ProcessNotificationsUsingBotAsync(
            Streams streams)
        {
            const string defaultConcern = "default-concern";

            var sendEmailProtocol = new SendEmailProtocol(SmtpServerConnectionDefinition);

            var sendSlackMessageProtocol = new SendSlackMessageProtocol(SlackAuthenticationToken);

            var uploadFileToSlackProtocol = new UploadFileToSlackProtocol(SlackAuthenticationToken);

            var getAudienceProtocol = new GetAudienceProtocol();

            var getDeliveryChannelConfigsProtocol = new GetDeliveryChannelConfigsProtocol();

            var prepareToSendOnChannelProtocol = new PrepareToSendOnChannelProtocol(streams);

            var sendNotificationHandler = new HandleSendNotificationProtocol(
                streams.NotificationEventStream,
                streams.NotificationSagaStream,
                getAudienceProtocol,
                getDeliveryChannelConfigsProtocol,
                prepareToSendOnChannelProtocol,
                CannotPrepareToSendOnChannelAction.StopAndNotDoNotSendOnAnyChannel,
                new Dictionary<IDeliveryChannel, IWriteOnlyStream>
                {
                    { new EmailDeliveryChannel(), streams.EmailOperationStream },
                    { new SlackDeliveryChannel(), streams.SlackOperationStream },
                },
                new UseInheritableTagsProtocol<SendNotificationRequestedEvent>(),
                new UseInheritableTagsProtocol<CouldNotGetOrUseAudienceEvent>(),
                new UseInheritableTagsProtocol<CouldNotGetOrUseDeliveryChannelConfigsEvent>(),
                new UseInheritableTagsProtocol<PrepareToSendNotificationEventBase>(),
                new UseInheritableTagsProtocol<ExecuteOpRequestedEvent<long, ProcessSendNotificationSagaOp>>());

            var sagaOperationHandler = new HandleProcessSendNotificationSagaProtocol(
                streams.NotificationEventStream,
                new Dictionary<IDeliveryChannel, IReadOnlyStream>
                {
                    { new EmailDeliveryChannel(), streams.EmailEventStream },
                    { new SlackDeliveryChannel(), streams.SlackEventStream },
                },
                new UseInheritableTagsProtocol<AttemptToSendNotificationEventBase>());

            var sendEmailHandler = new HandleSendEmailProtocol(sendEmailProtocol, streams.EmailEventStream);

            var sendSlackMessageHandler = new HandleSendSlackMessageProtocol(sendSlackMessageProtocol, streams.SlackEventStream);

            var uploadFileToSlackHandler = new HandleUploadFileToSlackProtocol(uploadFileToSlackProtocol, streams.SlackEventStream);

            // Act, Assert
            while (true)
            {
                await HandleUnhandledObjectOfTypeAsync(streams.ClientOperationStream, defaultConcern, sendNotificationHandler);

                await HandleUnhandledObjectOfTypeAsync(streams.NotificationSagaStream, defaultConcern, sagaOperationHandler);

                await HandleUnhandledObjectOfTypeAsync(streams.EmailOperationStream, defaultConcern, sendEmailHandler);

                await HandleUnhandledObjectOfTypeAsync(streams.SlackOperationStream, defaultConcern, sendSlackMessageHandler);

                await HandleUnhandledObjectOfTypeAsync(streams.SlackOperationStream, defaultConcern, uploadFileToSlackHandler);

                Thread.Sleep(1000);
            }
        }

        private static async Task<Streams> BuildStreamsAsync(
            string streamRootDirectoryPath)
        {
            var serializerRepresentation = new SerializerRepresentation(SerializationKind.Json, typeof(IntegrationTestJsonSerializationConfiguration).ToRepresentation());
            var serializerFactory = new JsonSerializerFactory();
            var resourceLocatorProtocols = new SingleResourceLocatorProtocol(new FileSystemDatabaseLocator(streamRootDirectoryPath));

            var clientOperationStream = new FileReadWriteStream("client-operation", serializerRepresentation, SerializationFormat.String, serializerFactory, resourceLocatorProtocols);
            var notificationEventStream = new FileReadWriteStream("notification-event", serializerRepresentation, SerializationFormat.String, serializerFactory, resourceLocatorProtocols);
            var notificationSagaStream = new FileReadWriteStream("notification-saga", serializerRepresentation, SerializationFormat.String, serializerFactory, resourceLocatorProtocols);
            var emailOperationStream = new FileReadWriteStream("email-operation", serializerRepresentation, SerializationFormat.String, serializerFactory, resourceLocatorProtocols);
            var emailEventStream = new FileReadWriteStream("email-event", serializerRepresentation, SerializationFormat.String, serializerFactory, resourceLocatorProtocols);
            var slackOperationStream = new FileReadWriteStream("slack-operation", serializerRepresentation, SerializationFormat.String, serializerFactory, resourceLocatorProtocols);
            var slackEventStream = new FileReadWriteStream("slack-event", serializerRepresentation, SerializationFormat.String, serializerFactory, resourceLocatorProtocols);

            await clientOperationStream.ExecuteAsync(new CreateStreamOp(clientOperationStream.StreamRepresentation, ExistingStreamEncounteredStrategy.Skip));
            await notificationEventStream.ExecuteAsync(new CreateStreamOp(notificationEventStream.StreamRepresentation, ExistingStreamEncounteredStrategy.Skip));
            await notificationSagaStream.ExecuteAsync(new CreateStreamOp(notificationSagaStream.StreamRepresentation, ExistingStreamEncounteredStrategy.Skip));
            await emailOperationStream.ExecuteAsync(new CreateStreamOp(emailOperationStream.StreamRepresentation, ExistingStreamEncounteredStrategy.Skip));
            await emailEventStream.ExecuteAsync(new CreateStreamOp(emailEventStream.StreamRepresentation, ExistingStreamEncounteredStrategy.Skip));
            await slackOperationStream.ExecuteAsync(new CreateStreamOp(slackOperationStream.StreamRepresentation, ExistingStreamEncounteredStrategy.Skip));
            await slackEventStream.ExecuteAsync(new CreateStreamOp(slackEventStream.StreamRepresentation, ExistingStreamEncounteredStrategy.Skip));

            var result = new Streams
            {
                ClientOperationStream = clientOperationStream,
                NotificationEventStream = notificationEventStream,
                NotificationSagaStream = notificationSagaStream,
                EmailOperationStream = emailOperationStream,
                EmailEventStream = emailEventStream,
                SlackOperationStream = slackOperationStream,
                SlackEventStream = slackEventStream,
            };

            return result;
        }

        private static async Task HandleUnhandledObjectOfTypeAsync<TObject>(
            FileReadWriteStream stream,
            string concern,
            HandleRecordAsyncSpecificProtocolBase<TObject> handler)
        {
            var streamRecordHandlingProtocol = stream.GetStreamRecordHandlingProtocols<TObject>();

            var recordToHandle = await streamRecordHandlingProtocol.ExecuteAsync(new TryHandleRecordOp<TObject>(concern));

            if (recordToHandle != null)
            {
                try
                {
                    var handleRecordOp = new HandleRecordOp<TObject>(recordToHandle);

                    await handler.ExecuteAsync(handleRecordOp);

                    stream.Execute(new CompleteRunningHandleRecordExecutionOp(recordToHandle.InternalRecordId, concern));
                }
                catch (SelfCancelRunningExecutionException ex)
                {
                    stream.Execute(new SelfCancelRunningHandleRecordExecutionOp(recordToHandle.InternalRecordId, concern, ex.Details));
                }
                catch (Exception ex)
                {
                    stream.Execute(new FailRunningHandleRecordExecutionOp(recordToHandle.InternalRecordId, concern, ex.ToString()));

                    throw;
                }
            }
        }

        private class Streams
        {
            public FileReadWriteStream ClientOperationStream { get; set; }

            public FileReadWriteStream NotificationEventStream { get; set; }

            public FileReadWriteStream NotificationSagaStream { get; set; }

            public FileReadWriteStream EmailOperationStream { get; set; }

            public FileReadWriteStream EmailEventStream { get; set; }

            public FileReadWriteStream SlackOperationStream { get; set; }

            public FileReadWriteStream SlackEventStream { get; set; }
        }

        private class IntegrationTestNotification : NotificationBase
        {
            public string ScenarioBeingTested { get; set; }
        }

        private class IntegrationTestAudience : AudienceBase
        {
            public string SenderEmailAddress { get; set; }

            public string RecipientEmailAddress { get; set; }

            public string SlackChannelId { get; set; }
        }

        private class IntegrationTestJsonSerializationConfiguration : JsonSerializationConfigurationBase
        {
            /// <inheritdoc />
            protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
            {
                typeof(IntegrationTestNotification).ToTypeToRegisterForJson(),
                typeof(IntegrationTestAudience).ToTypeToRegisterForJson(),
            };

            /// <inheritdoc />
            protected override IReadOnlyCollection<JsonSerializationConfigurationType> DependentJsonSerializationConfigurationTypes =>
                new[]
                {
                    typeof(NotificationJsonSerializationConfiguration).ToJsonSerializationConfigurationType(),
                    typeof(DatabaseJsonSerializationConfiguration).ToJsonSerializationConfigurationType(),
                    typeof(SlackJsonSerializationConfiguration).ToJsonSerializationConfigurationType(),
                    typeof(EmailJsonSerializationConfiguration).ToJsonSerializationConfigurationType(),
                };
        }

        private class GetAudienceProtocol : IGetAudienceProtocol
        {
            public async Task<GetAudienceResult> ExecuteAsync(
                GetAudienceOp operation)
            {
                new { operation }.AsArg().Must().NotBeNull();

                new { operation.Notification }.AsArg().Must().NotBeNull().And().BeOfType<IntegrationTestNotification>();

                var getAudienceResult = new GetAudienceResult(
                    Audience,
                    new[]
                    {
                        new ExceptionThrownFailure("something-bad-happened"),
                    },
                    FailureAction.IgnoreAndProceedIfPossibleOtherwiseStop);

                var result = await Task.FromResult(getAudienceResult);

                return result;
            }
        }

        private class GetDeliveryChannelConfigsProtocol : IGetDeliveryChannelConfigsProtocol
        {
            public async Task<GetDeliveryChannelConfigsResult> ExecuteAsync(
                GetDeliveryChannelConfigsOp operation)
            {
                new { operation }.AsArg().Must().NotBeNull();
                new { operation.Notification }.AsArg().Must().NotBeNull().And().BeOfType<IntegrationTestNotification>();
                new { operation.Audience }.AsArg().Must().NotBeNull().And().BeOfType<IntegrationTestAudience>();

                var getDeliveryChannelConfigsResult = new GetDeliveryChannelConfigsResult(
                    new[]
                    {
                        new DeliveryChannelConfig(new EmailDeliveryChannel(), DeliveryChannelAction.SendOnChannel),
                        new DeliveryChannelConfig(new SlackDeliveryChannel(), DeliveryChannelAction.SendOnChannel),
                    },
                    new[]
                    {
                        new ExceptionThrownFailure("exception-thrown-get-delivery-channel-configs"),
                    },
                    FailureAction.IgnoreAndProceedIfPossibleOtherwiseStop);

                var result = await Task.FromResult(getDeliveryChannelConfigsResult);

                return result;
            }
        }

        private class PrepareToSendOnChannelProtocol : IPrepareToSendOnChannelProtocol
        {
            private readonly Streams streams;

            public PrepareToSendOnChannelProtocol(
                Streams streams)
            {
                new { streams }.AsArg().Must().NotBeNull();

                this.streams = streams;
            }

            public async Task<PrepareToSendOnChannelResult> ExecuteAsync(
                PrepareToSendOnChannelOp operation)
            {
                new { operation }.AsArg().Must().NotBeNull();
                new { operation.Notification }.AsArg().Must().NotBeNull().And().BeOfType<IntegrationTestNotification>();
                new { operation.Audience }.AsArg().Must().NotBeNull().And().BeOfType<IntegrationTestAudience>();
                new { operation.DeliveryChannel }.AsArg().Must().NotBeNull();
                new { operation.InheritableTags }.AsArg().Must().NotBeNullNorEmptyDictionaryNorContainAnyNullValues();

                IReadOnlyList<ChannelOperationInstruction> channelOperationInstructions;

                var audience = (IntegrationTestAudience)operation.Audience;

                var notification = (IntegrationTestNotification)operation.Notification;

                if (operation.DeliveryChannel is EmailDeliveryChannel)
                {
                    var trackingCodeId = await this.streams.EmailOperationStream.GetNextUniqueLongAsync();

                    var emailParticipants = new EmailParticipants(
                        new EmailMailbox(audience.SenderEmailAddress),
                        new[] { new EmailMailbox(audience.RecipientEmailAddress) });

                    var emailContent = new EmailContent(notification.ScenarioBeingTested, "Here is the body for scenario: " + notification.ScenarioBeingTested);

                    var tags = (operation.InheritableTags.DeepClone() ?? new List<NamedValue<string>>()).ToList();
                    tags.Add(new NamedValue<string>("recipient-email-address", emailParticipants.To.First().Address));
                    tags.Add(new NamedValue<string>("sender-email-address", emailParticipants.From.Address));

                    channelOperationInstructions = new[]
                    {
                        new ChannelOperationInstruction(
                            new SendEmailOp(new SendEmailRequest(emailParticipants, emailContent)),
                            new ChannelOperationMonitoringInfo(
                                trackingCodeId,
                                typeof(SucceededInSendingEmailEvent<long>).ToRepresentation(),
                                typeof(FailedToSendEmailEvent<long>).ToRepresentation()),
                            tags),
                    };
                }
                else if (operation.DeliveryChannel is SlackDeliveryChannel)
                {
                    var sendMessageTrackingCodeId = await this.streams.SlackOperationStream.GetNextUniqueLongAsync();

                    var uploadFileTrackingCodeId = await this.streams.SlackOperationStream.GetNextUniqueLongAsync();

                    var tags = (operation.InheritableTags.DeepClone() ?? new List<NamedValue<string>>()).ToList();
                    tags.Add(new NamedValue<string>("slack-channel-id", audience.SlackChannelId));

                    var fileBytes = AssemblyHelper.ReadEmbeddedResourceAsBytes("test-file-png");

                    channelOperationInstructions = new[]
                    {
                        new ChannelOperationInstruction(
                            new SendSlackMessageOp(new SendSlackTextMessageRequest(audience.SlackChannelId, notification.ScenarioBeingTested)),
                            new ChannelOperationMonitoringInfo(
                                sendMessageTrackingCodeId,
                                typeof(SucceededInSendingSlackMessageEvent<long>).ToRepresentation(),
                                typeof(FailedToSendSlackMessageEvent<long>).ToRepresentation()),
                            tags),
                        new ChannelOperationInstruction(
                            new UploadFileToSlackOp(new UploadFileToSlackRequest(fileBytes, new[] { audience.SlackChannelId }, FileType.Png)),
                            new ChannelOperationMonitoringInfo(
                                uploadFileTrackingCodeId,
                                typeof(SucceededInUploadingFileToSlackEvent<long>).ToRepresentation(),
                                typeof(FailedToUploadFileToSlackEvent<long>).ToRepresentation()),
                            tags),
                    };
                }
                else
                {
                    throw new NotSupportedException("This delivery channel is not supported: " + operation.DeliveryChannel.GetType().ToStringReadable());
                }

                var prepareToSendOnChannelResult = new PrepareToSendOnChannelResult(
                    channelOperationInstructions,
                    new[]
                    {
                        new ExceptionThrownFailure("exception-thrown-prepare-to-send-on-channel-" + operation.DeliveryChannel.GetType().ToStringReadable()),
                    },
                    PrepareToSendOnChannelFailureAction.IgnoreAndProceedIfPossibleOtherwiseDoNotSendOnChannel);

                var result = await Task.FromResult(prepareToSendOnChannelResult);

                return result;
            }
        }

        private class BuildExecuteSendNotificationEventTagsProtocol : IBuildTagsProtocol<ExecuteOpRequestedEvent<long, SendNotificationOp>>
        {
            public IReadOnlyCollection<NamedValue<string>> Execute(
                BuildTagsOp<ExecuteOpRequestedEvent<long, SendNotificationOp>> operation)
            {
                new { operation }.AsArg().Must().NotBeNull();
                new { operation.InheritableTags }.AsArg().Must().BeNull();
                new { operation.Event }.AsArg().Must().NotBeNull();
                new { operation.Event.Operation }.AsArg().Must().NotBeNull();
                new { operation.Event.Operation.Notification }.AsArg().Must().NotBeNull().And().BeOfType<IntegrationTestNotification>();

                var result = new List<NamedValue<string>>
                             {
                                 new NamedValue<string>("notification-type", operation.Event.Operation.Notification.GetType().ToStringReadable()),
                                 new NamedValue<string>("random-data", "random-" + Guid.NewGuid()),
                             };

                if (operation.Event.Operation.Notification is IntegrationTestNotification notification)
                {
                    result.Add(new NamedValue<string>("scenario", notification.ScenarioBeingTested));
                }

                return result;
            }
        }
    }
}
