using DemoStore.Services.CommandSide.Application.Common.Contracts;
using DemoStore.Services.CommandSide.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using DemoStore.Services.CommandSide.Infrastructure.Common.Contracts;
using DemoStore.Services.CommandSide.Infrastructure.IntegrationEvents;

namespace DemoStore.Services.CommandSide.Infrastructure.Persistence.Interceptors;

internal sealed class EventDispatchingInterceptor : SaveChangesInterceptor
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IMessageBrokerPublisher _messageBrokerPublisher;

    public EventDispatchingInterceptor(
        IEventDispatcher eventDispatcher,
        IMessageBrokerPublisher messageBrokerPublisher)
    {
        _eventDispatcher = eventDispatcher;
        _messageBrokerPublisher = messageBrokerPublisher;
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await DispatchEventsAsync(eventData.Context, cancellationToken);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }


    private async Task DispatchEventsAsync(DbContext dbContext, CancellationToken token = default)
    {

        var domainEvents = dbContext.ChangeTracker.Entries<Entity>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _eventDispatcher.DispatchAsync(domainEvent, token);

            var integrationEvent = domainEvent.ToIntegrationEvent();

            if (integrationEvent is not null)
                await _messageBrokerPublisher.PublishAsync(integrationEvent);
        }
    }
}