using DemoStore.Services.QuerySide.Common;
using MediatR;

namespace DemoStore.Services.QuerySide.Products.IntegrationEvents;

internal class NewProductCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public decimal Price { get; init; }

    public int Quantity { get; init; }

    public string ThumbnailUrl { get; init; }

    public NewProductCreatedIntegrationEvent()
    {
        Type = nameof(NewProductCreatedIntegrationEvent);
    }
}

internal class NewProductCreatedIntegrationEventHandler : INotificationHandler<NewProductCreatedIntegrationEvent>
{
    private readonly IRepository<Product> _repository;

    public NewProductCreatedIntegrationEventHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task Handle(NewProductCreatedIntegrationEvent integrationEvent, CancellationToken token)
    {
        var product = new Product
        (
            integrationEvent.Id,
            integrationEvent.Name,
            integrationEvent.Price,
            integrationEvent.Quantity,
            integrationEvent.ThumbnailUrl
        );

        await _repository.AddAsync(product, token);
    }
}