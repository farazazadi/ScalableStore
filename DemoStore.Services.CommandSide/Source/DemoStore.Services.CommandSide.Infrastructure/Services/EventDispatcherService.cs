using DemoStore.Services.CommandSide.Application.Common.Contracts;
using DemoStore.Services.CommandSide.Application.Common.Models;
using DemoStore.Services.CommandSide.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DemoStore.Services.CommandSide.Infrastructure.Services;

internal sealed class EventDispatcherService : IEventDispatcher
{
    private readonly ILogger<EventDispatcherService> _logger;
    private readonly IMediator _mediator;

    public EventDispatcherService(ILogger<EventDispatcherService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task DispatchAsync(DomainEvent domainEvent, CancellationToken token = default)
    {
        _logger.LogInformation("Publishing {BoundedContext}'s domain events. Event: {event}", "CommandSide", domainEvent.GetType().Name);

        var notification = CreateNotification(domainEvent);

        await _mediator.Publish(notification);
    }

    private INotification CreateNotification(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(typeof(Notification<>)
            .MakeGenericType(domainEvent.GetType()), domainEvent);
    }
}