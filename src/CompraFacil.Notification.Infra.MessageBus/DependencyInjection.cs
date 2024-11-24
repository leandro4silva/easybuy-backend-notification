using CompraFacil.Notification.Infra.Configurations;
using CompraFacil.Notification.Infra.MessageBus.Clients.RabbitMq;
using CompraFacil.Notification.Infra.MessageBus.Subscribers;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CompraFacil.Notification.Infra.MessageBus;

public static class DependencyInjection
{
    public static async Task<IServiceCollection> AddRabbitMqAsync(this IServiceCollection services, AppConfiguration appConfiguration)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = appConfiguration.RabbitMq?.HostName!,
            Port = appConfiguration.RabbitMq!.Port,
            UserName = appConfiguration.RabbitMq.User!,
            Password = appConfiguration.RabbitMq.Password!
        };

        var connection = await connectionFactory.CreateConnectionAsync("notifications-service-order-created-consumer");

        services.AddSingleton(new ProducerConnection(connection));

        return services;
    }

    public static IServiceCollection AddSubscribers(this IServiceCollection services)
    {
        services.AddHostedService<CustomerCreatedSubscriber>();

        return services;
    }
}
