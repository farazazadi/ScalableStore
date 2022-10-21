using DemoStore.Services.QuerySide.Common;

namespace DemoStore.Services.QuerySide.Products;

public class Product : IDocument
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ExternalId { get; private set; }

    public string Name { get; private set; }

    public decimal Price { get; private set; }

    public int Quantity { get; private set; }

    public string ThumbnailUrl { get; private set; }

    public Product(Guid externalId, string name, decimal price, int quantity, string thumbnailUrl)
    {
        ExternalId = externalId;
        Name = name;
        Price = price;
        Quantity = quantity;
        ThumbnailUrl = thumbnailUrl;
    }

    public void Buy(int quantity)
    {
        Quantity -= quantity;
    }
}
