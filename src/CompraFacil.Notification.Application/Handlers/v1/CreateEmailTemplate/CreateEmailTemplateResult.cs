using System.Text.Json.Serialization;

namespace CompraFacil.Notification.Application.Handlers.v1.CreateTemplate;

public sealed class CreateEmailTemplateResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("Subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("event")]
    public string? Event { get; set; }
}
