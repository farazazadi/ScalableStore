using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Application.Common.Contracts;

public interface IEventDispatcher
{
    Task DispatchAsync(DomainEvent domainEvent, CancellationToken token);
}