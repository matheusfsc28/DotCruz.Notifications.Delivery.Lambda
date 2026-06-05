using DotCruz.Notifications.Delivery.Lambda.Interfaces;
using DotCruz.Notifications.Delivery.Lambda.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace DotCruz.Notifications.Delivery.Lambda.Services;

public class EmailSenderStrategy : INotificationSenderStrategy
{
    public string HandledType => "Email";

    public async Task SendAsync(NotificationPayload payload, CancellationToken cancellationToken)
    {
        var email = new MimeMessage();
        var fromEmail = Environment.GetEnvironmentVariable("SMTP_USERNAME") 
            ?? throw new InvalidOperationException("SMTP_USERNAME env variable is not set.");
        
        email.From.Add(new MailboxAddress("DotCruz Notifications", fromEmail));
        email.To.Add(new MailboxAddress(string.Empty, payload.Recipient));
        email.Subject = payload.Title;

        var bodyBuilder = new BodyBuilder { HtmlBody = payload.Body };
        email.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(
            Environment.GetEnvironmentVariable("SMTP_HOST") ?? throw new InvalidOperationException("SMTP_HOST env variable is not set."),
            int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587"),
            false,
            cancellationToken
        );
        
        var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD")
            ?? throw new InvalidOperationException("SMTP_PASSWORD env variable is not set.");
        
        await client.AuthenticateAsync(fromEmail, smtpPassword, cancellationToken);
        await client.SendAsync(email, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}
