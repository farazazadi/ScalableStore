using DemoStore.Services.QuerySide.Common;
using MediatR;

namespace DemoStore.Services.QuerySide.Products.IntegrationEvents;

internal class ProductBoughtIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }

    public ProductBoughtIntegrationEvent()
    {
        Type = nameof(ProductBoughtIntegrationEvent);
    }
}

internal class ProductBoughtIntegrationEventHandler : INotificationHandler<ProductBoughtIntegrationEvent>
{
    private readonly IRepository<Product> _repository;

    public ProductBoughtIntegrationEventHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }
    public async Task Handle(ProductBoughtIntegrationEvent integrationEvent, CancellationToken token)
    {
        var product = await _repository.GetAsync(p => p.ExternalId == integrationEvent.Id, token)
                      ?? throw new Exception($"Product with Id({integrationEvent.Id}) was not found!");

        product.Buy(integrationEvent.Quantity);

        await _repository.UpdateAsync(product, token);
    }
}