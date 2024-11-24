using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;

namespace CompraFacil.Notification.Infra.MessageBus.Clients.RabbitMq;

[ExcludeFromCodeCoverage]
public sealed class ProducerConnection
{
    public IConnection Connection { get; private set; }

    public ProducerConnection(IConnection connection)
    {
        Connection = connection;
    }
}
