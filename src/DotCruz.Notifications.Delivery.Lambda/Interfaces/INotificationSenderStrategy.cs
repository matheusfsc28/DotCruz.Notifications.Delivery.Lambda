using System.Threading;
using System.Threading.Tasks;
using DotCruz.Notifications.Delivery.Lambda.Models;

namespace DotCruz.Notifications.Delivery.Lambda.Interfaces;

public interface INotificationSenderStrategy
{
    string HandledType { get; }
    Task SendAsync(NotificationPayload payload, CancellationToken cancellationToken);
}
