using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Domain.Products.Events;
public sealed class ProductBoughtEvent : DomainEvent
{
    public Guid ProductId { get; }
    public ProductQuantity Quantity { get; }

    public ProductBoughtEvent(Guid productId, ProductQuantity quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}