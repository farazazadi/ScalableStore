namespace DemoStore.Services.CommandSide.Infrastructure.Common;

public abstract class IntegrationEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public abstract string Type { get;}
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}