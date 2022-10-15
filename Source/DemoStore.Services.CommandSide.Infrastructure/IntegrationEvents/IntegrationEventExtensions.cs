using DemoStore.Services.CommandSide.Domain.Common;
using DemoStore.Services.CommandSide.Domain.Products.Events;
using DemoStore.Services.CommandSide.Infrastructure.IntegrationEvents.Products;

namespace DemoStore.Services.CommandSide.Infrastructure.IntegrationEvents;
internal static class IntegrationEventExtensions
{
    public static dynamic ToIntegrationEvent(this DomainEvent domainEvent)
    {
        return domainEvent switch
        {
            NewProductCreatedEvent e => new NewProductCreatedIntegrationEvent
            {
                Id = e.Product.Id,
                Name = e.Product.Name,
                Price = e.Product.Price,
                Quantity = e.Product.Quantity,
                ThumbnailUrl = e.Product.ThumbnailUrl
            },

            _ => null
        };
    }
}
