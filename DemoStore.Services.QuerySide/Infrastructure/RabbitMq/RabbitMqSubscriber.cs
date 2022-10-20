using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DemoStore.Services.QuerySide.Infrastructure.RabbitMq;

internal sealed class RabbitMqSubscriber : BackgroundService
{
    private readonly IPublisher _publisher;

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public RabbitMqSubscriber(IPublisher publisher, IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _publisher = publisher;

        var options = rabbitMqOptions.Value;

        var connectionFactory = new ConnectionFactory
        {
            HostName = options.HostName,
            Port = options.Port,
            UserName = options.UserName,
            Password = options.Password,
            VirtualHost = options.VirtualHost,
            AutomaticRecoveryEnabled = options.AutomaticRecoveryEnabled,
            DispatchConsumersAsync = true
        };

        _queueName = "Synchronization";

        _connection = connectionFactory.CreateConnection("QuerySideService");
        _channel = _connection.CreateModel();
        _channel.BasicQos(0, 1, true);
        _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (sender, args) =>
        {
            var message = Encoding.UTF8.GetString(args.Body.ToArray());

            try
            {
                await _publisher.Publish(message.ToIntegrationEvent());

                _channel.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception)
            {
                _channel.BasicNack(args.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(_queueName, autoAck: false, consumer);

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken token)
    {
        _channel.Close();
        _connection.Close();

        await base.StopAsync(token);
    }
}