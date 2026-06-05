using DotCruz.Notifications.Delivery.Lambda.Models;
using MediatR;

namespace DotCruz.Notifications.Delivery.Lambda.UseCases.ProcessNotification;

public record ProcessNotificationCommand(
    NotificationPayload Payload
) : IRequest;
