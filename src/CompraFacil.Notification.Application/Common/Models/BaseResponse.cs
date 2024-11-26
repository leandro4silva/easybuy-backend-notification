using System.Text.Json.Serialization;

namespace CompraFacil.Notification.Application.Common.Models;

public class BaseResponse<TData>
{
    [JsonPropertyName("data")]
    public TData? Data { get; private set; }
}
