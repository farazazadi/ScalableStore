using System.Text;
using System.Text.Json;
using DemoStore.Services.CommandSide.Infrastructure.Common.Contracts;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DemoStore.Services.CommandSide.Infrastructure.Services.RabbitMq;
internal class RabbitMqPublisher : IMessageBrokerPublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchangeName;
    private readonly string _routingKey = string.Empty;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RabbitMq");

        var connectionFactory = new ConnectionFactory
        {
            Uri = new Uri(connectionString),
            AutomaticRecoveryEnabled = true,
            DispatchConsumersAsync = true
        };

        _connection = connectionFactory.CreateConnection("CommandSideService");

        _exchangeName = "CommandSide";
        var queueName = "Synchronization";

        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout, true);
        _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(queueName, _exchangeName, _routingKey);

    }
    public Task PublishAsync<T>(T message) where T : class
    {
        _channel.BasicPublish(_exchangeName, _routingKey, body: GetBytes(message));
        return Task.CompletedTask;
    }

    private static byte[] GetBytes<T>(T message) where T : class
    {
        var jsonString = JsonSerializer.Serialize(message);
        return Encoding.UTF8.GetBytes(jsonString);
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
            _channel.Close();

        if (_connection.IsOpen)
            _connection.Close();

        _channel?.Dispose();
        _connection?.Dispose();
    }
}
