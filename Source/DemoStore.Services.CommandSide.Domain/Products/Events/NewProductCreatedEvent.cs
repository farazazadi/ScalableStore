using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Domain.Products.Events;

public sealed class NewProductCreatedEvent : DomainEvent
{
    private readonly Product _product;

    public NewProductCreatedEvent(Product product)
    {
        _product = product;
    }
}
