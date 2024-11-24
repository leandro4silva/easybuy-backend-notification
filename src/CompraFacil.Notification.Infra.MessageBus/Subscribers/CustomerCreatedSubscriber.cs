using CompraFacil.Notification.Domain.Events;
using CompraFacil.Notification.Domain.Repositories;
using CompraFacil.Notification.Infra.MessageBus.Clients.RabbitMq;
using CompraFacil.Notification.Infra.SendGrid.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CompraFacil.Notification.Infra.MessageBus.Subscribers;

public class CustomerCreatedSubscriber : BackgroundService
{
    private readonly ProducerConnection _connection;
    private readonly ILogger<CustomerCreatedSubscriber> _logger;
    private readonly IMailRepository _mailRepository;
    private readonly ISendMailService _sendMailService;
    private const string Queue = "notification-service/customer-created";
    private const string Exchange = "notification-service";

    public CustomerCreatedSubscriber(
        ProducerConnection connection, 
        ILogger<CustomerCreatedSubscriber> logger,
        IMailRepository mailRepository,
        ISendMailService sendMailService
    )
    {
        _connection = connection;
        _logger = logger;
        _mailRepository = mailRepository;
        _sendMailService = sendMailService;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var channel = await _connection.Connection.CreateChannelAsync(cancellationToken: cancellationToken);

        try
        {
            await channel.ExchangeDeclareAsync(Exchange, "topic", durable: true, autoDelete: true);
            await channel.QueueDeclareAsync(Queue, durable: false, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(Queue, Exchange, Queue);

            await channel.QueueBindAsync(Queue, "customer-service", "customer-created");

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                try
                {
                    var contentArray = eventArgs.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var message = JsonConvert.DeserializeObject<CustomerCreated>(contentString);

                    if (message == null)
                    {
                        _logger.LogWarning("Received a null or invalid message. Skipping...");
                        return;
                    }

                    _logger.LogInformation($"Message CustomerCreated received with Id {message.Id}");

                    await SendEmail(message);

                    await channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the message.");
                    await channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: true);
                }
            };

            await channel.BasicConsumeAsync(Queue, autoAck: false, consumer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during the initialization of the subscriber.");
        }
        finally
        {
            channel.Dispose();
        }
    }

    private async Task<bool> SendEmail(CustomerCreated customer)
    {
        var template = await _mailRepository.GetTemplate("CustomerCreated");

        var subject = string.Format(template.Subject!, customer.FullName);
        var content = string.Format(template.Content!, customer.FullName);

        await _sendMailService.SendAsync(subject, content, customer.Email!, customer.FullName!);

        return true;
        
    }
}
