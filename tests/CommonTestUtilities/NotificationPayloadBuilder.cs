using System;
using DotCruz.Notifications.Delivery.Lambda.Models;

namespace CommonTestUtilities;

public class NotificationPayloadBuilder
{
    private Guid _notificationId = Guid.NewGuid();
    private string _type = "Email";
    private string _recipient = "test@example.com";
    private string _title = "Default Title";
    private string _body = "Default Body";

    public NotificationPayloadBuilder WithNotificationId(Guid notificationId)
    {
        _notificationId = notificationId;
        return this;
    }

    public NotificationPayloadBuilder WithType(string type)
    {
        _type = type;
        return this;
    }

    public NotificationPayloadBuilder WithRecipient(string recipient)
    {
        _recipient = recipient;
        return this;
    }

    public NotificationPayloadBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public NotificationPayloadBuilder WithBody(string body)
    {
        _body = body;
        return this;
    }

    public NotificationPayload Build()
    {
        return new NotificationPayload
        {
            NotificationId = _notificationId,
            Type = _type,
            Recipient = _recipient,
            Title = _title,
            Body = _body
        };
    }
}
