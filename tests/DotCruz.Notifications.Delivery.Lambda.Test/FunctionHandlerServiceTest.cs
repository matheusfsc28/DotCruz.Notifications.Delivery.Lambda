using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using CommonTestUtilities;
using DotCruz.Notifications.Delivery.Lambda;
using DotCruz.Notifications.Delivery.Lambda.Interfaces;
using DotCruz.Notifications.Delivery.Lambda.Models;
using DotCruz.Notifications.Delivery.Lambda.UseCases.ProcessNotification;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace DotCruz.Notifications.Delivery.Lambda.Test;

public class FunctionHandlerServiceTest
{
    [Fact]
    public async Task FunctionHandler_Should_Dispatch_ProcessNotificationCommand_To_Mediator()
    {
        // Arrange
        var mockMediator = Substitute.For<IMediator>();
        var handlerService = new FunctionHandlerService(mockMediator);

        var payload = new NotificationPayloadBuilder()
            .WithType("Email")
            .WithRecipient("user@example.com")
            .Build();

        var sqsEvent = new SQSEvent
        {
            Records = new List<SQSEvent.SQSMessage>
            {
                new() { Body = JsonSerializer.Serialize(payload) }
            }
        };

        var mockContext = Substitute.For<ILambdaContext>();
        var mockLogger = Substitute.For<ILambdaLogger>();
        mockContext.Logger.Returns(mockLogger);

        // Act
        await handlerService.FunctionHandler(sqsEvent, mockContext);

        // Assert
        await mockMediator.Received(1).Send(
            Arg.Is<ProcessNotificationCommand>(c => 
                c.NotificationId == payload.NotificationId &&
                c.Type == payload.Type &&
                c.Recipient == payload.Recipient &&
                c.Title == payload.Title &&
                c.Body == payload.Body
            ), 
            Arg.Any<CancellationToken>()
        );
    }
}

public class ProcessNotificationCommandHandlerTest
{
    [Fact]
    public async Task Handle_Should_ExecuteStrategy_And_SendCallbackStatus()
    {
        // Arrange
        var mockSender = Substitute.For<INotificationSenderStrategy>();
        mockSender.HandledType.Returns("Email");
        var senders = new List<INotificationSenderStrategy> { mockSender };

        var mockClient = Substitute.For<INotificationClient>();
        var mockLogger = Substitute.For<ILogger<ProcessNotificationCommandHandler>>();

        var handler = new ProcessNotificationCommandHandler(senders, mockClient, mockLogger);

        var command = new ProcessNotificationCommand(
            NotificationId: Guid.NewGuid(),
            Type: "Email",
            Recipient: "user@example.com",
            Title: "Welcome",
            Body: "Hello User"
        );

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await mockSender.Received(1).SendAsync(
            Arg.Is<NotificationPayload>(p => 
                p.NotificationId == command.NotificationId && 
                p.Type == command.Type &&
                p.Recipient == command.Recipient
            ), 
            Arg.Any<CancellationToken>()
        );

        await mockClient.Received(1).UpdateStatusAsync(
            command.NotificationId,
            success: true,
            errorMessage: null,
            Arg.Any<CancellationToken>()
        );
    }
}
