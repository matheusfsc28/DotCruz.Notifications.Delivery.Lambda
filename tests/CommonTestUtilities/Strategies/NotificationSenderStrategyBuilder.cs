using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using DotCruz.Notifications.Delivery.Lambda.Interfaces;
using DotCruz.Notifications.Delivery.Lambda.Models;

namespace CommonTestUtilities.Strategies;

public class NotificationSenderStrategyBuilder
{
    private readonly Mock<INotificationSenderStrategy> _strategy;

    public NotificationSenderStrategyBuilder(string handledType)
    {
        _strategy = new Mock<INotificationSenderStrategy>();
        _strategy.Setup(s => s.HandledType).Returns(handledType);
    }

    public NotificationSenderStrategyBuilder SetupSend()
    {
        _strategy.Setup(s => s.SendAsync(It.IsAny<NotificationPayload>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        return this;
    }

    public NotificationSenderStrategyBuilder SetupSendThrows(Exception exception)
    {
        _strategy.Setup(s => s.SendAsync(It.IsAny<NotificationPayload>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);
        return this;
    }

    public INotificationSenderStrategy Build() => _strategy.Object;
    public Mock<INotificationSenderStrategy> GetMock() => _strategy;
}
