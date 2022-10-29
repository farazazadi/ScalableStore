using DemoStore.Services.CommandSide.Infrastructure.Common;

namespace DemoStore.Services.CommandSide.Infrastructure.IntegrationEvents.Products;
internal class ProductBoughtIntegrationEvent : IntegrationEvent
{
    public override string Type => nameof(ProductBoughtIntegrationEvent);

    public Guid Id { get; init; }
    public int Quantity { get; init; }
}
