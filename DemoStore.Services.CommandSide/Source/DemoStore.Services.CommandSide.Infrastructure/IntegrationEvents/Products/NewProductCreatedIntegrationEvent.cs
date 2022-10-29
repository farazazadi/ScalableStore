using DemoStore.Services.CommandSide.Infrastructure.Common;

namespace DemoStore.Services.CommandSide.Infrastructure.IntegrationEvents.Products;

internal class NewProductCreatedIntegrationEvent : IntegrationEvent
{
    public override string Type => nameof(NewProductCreatedIntegrationEvent);

    public Guid Id { get; init; }
    public string Name { get; init; }

    public decimal Price { get; init; }

    public int Quantity { get; init; }

    public string ThumbnailUrl { get; init; }
}
