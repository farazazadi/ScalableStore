using DemoStore.Services.CommandSide.Domain.Common;
using MediatR;

namespace DemoStore.Services.CommandSide.Application.Common.Models;

public class Notification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    public TDomainEvent DomainEvent { get; }

    public Notification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}