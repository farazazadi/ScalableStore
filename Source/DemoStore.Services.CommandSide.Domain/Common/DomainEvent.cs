namespace DemoStore.Services.CommandSide.Domain.Common;

public abstract class DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;

}