using DemoStore.Services.CommandSide.Application.Common.Contracts;
using DemoStore.Services.CommandSide.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace DemoStore.Services.CommandSide.Infrastructure.Persistence.Interceptors;

internal sealed class EventDispatchingInterceptor : SaveChangesInterceptor
{
    private readonly IEventDispatcher _eventDispatcher;

    public EventDispatchingInterceptor(IEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
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
            await _eventDispatcher.DispatchAsync(domainEvent, token);
    }
}