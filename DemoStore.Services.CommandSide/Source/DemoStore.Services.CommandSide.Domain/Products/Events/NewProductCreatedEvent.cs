using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Domain.Products.Events;

public sealed class NewProductCreatedEvent : DomainEvent
{
    public Product Product { get; }

    public NewProductCreatedEvent(Product product)
    {
        Product = product;
    }
}
