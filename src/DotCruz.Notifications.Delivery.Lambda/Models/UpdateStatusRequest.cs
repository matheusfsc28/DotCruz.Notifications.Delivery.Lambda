using System;

namespace DotCruz.Notifications.Delivery.Lambda.Models;

public class UpdateStatusRequest
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
