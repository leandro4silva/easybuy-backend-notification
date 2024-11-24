using System.Diagnostics.CodeAnalysis;

namespace CompraFacil.Notification.Infra.Configurations;

[ExcludeFromCodeCoverage]
public class MailConfiguration
{
    public string? Host { get; set; }

    public int Port { get; set; }

    public string? FromName { get; set; }

    public string? FromEmail { get; set; }

    public string? Password { get; set; }
}
