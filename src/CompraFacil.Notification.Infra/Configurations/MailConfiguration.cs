using System.Diagnostics.CodeAnalysis;

namespace CompraFacil.Notification.Infra.Configurations;

[ExcludeFromCodeCoverage]
public class MailConfiguration
{
    public string? SendGridApiKey { get; set; }
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
}
