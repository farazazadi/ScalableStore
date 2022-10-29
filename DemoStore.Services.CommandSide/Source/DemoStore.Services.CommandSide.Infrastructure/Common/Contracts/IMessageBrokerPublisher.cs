namespace DemoStore.Services.CommandSide.Infrastructure.Common.Contracts;
public interface IMessageBrokerPublisher
{
    Task PublishAsync<T>(T message) where T : class;
}