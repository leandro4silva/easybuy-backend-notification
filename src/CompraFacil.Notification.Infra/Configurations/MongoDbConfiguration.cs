using System.Diagnostics.CodeAnalysis;

namespace CompraFacil.Notification.Infra.Configurations;

[ExcludeFromCodeCoverage]
public sealed class MongoDbConfiguration
{
    public string? Database { get; set; }

    public string? ConnectionString { get; set; }
}
