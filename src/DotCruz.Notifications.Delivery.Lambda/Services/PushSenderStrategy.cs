using System;
using System.Threading;
using System.Threading.Tasks;
using DotCruz.Notifications.Delivery.Lambda.Interfaces;
using DotCruz.Notifications.Delivery.Lambda.Models;

namespace DotCruz.Notifications.Delivery.Lambda.Services;

public class PushSenderStrategy : INotificationSenderStrategy
{
    public string HandledType => "Push";

    public Task SendAsync(NotificationPayload payload, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Push delivery is not implemented yet.");
    }
}
