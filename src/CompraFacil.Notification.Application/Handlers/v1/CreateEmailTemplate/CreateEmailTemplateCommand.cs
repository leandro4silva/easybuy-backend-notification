using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace CompraFacil.Notification.Application.Handlers.v1.CreateTemplate;

public sealed class CreateEmailTemplateCommand : IRequest<CreateEmailTemplateResult>
{
    [FromBody]
    public Payload? Payload { get; set; } 
}

public sealed class Payload
{
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonIgnore]
    public string? Event
    {
        get => "CustomerCreated";
    }
}