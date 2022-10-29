using MediatR;

namespace DemoStore.Services.QuerySide.Common;

internal class IntegrationEvent : INotification
{
    public Guid EventId { get; set; }
    public string Type { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
}