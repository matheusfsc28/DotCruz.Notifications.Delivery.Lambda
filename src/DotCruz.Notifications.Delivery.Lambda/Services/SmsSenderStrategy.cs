using DotCruz.Notifications.Delivery.Lambda.Interfaces;
using DotCruz.Notifications.Delivery.Lambda.Models;

namespace DotCruz.Notifications.Delivery.Lambda.Services;

public class SmsSenderStrategy : INotificationSenderStrategy
{
    public string HandledType => "Sms";

    public Task SendAsync(NotificationPayload payload, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("SMS delivery is not implemented yet.");
    }
}
