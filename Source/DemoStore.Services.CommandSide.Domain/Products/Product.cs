using DemoStore.Services.CommandSide.Domain.Common;
using DemoStore.Services.CommandSide.Domain.Common.ValueObjects;
using DemoStore.Services.CommandSide.Domain.Products.Events;

namespace DemoStore.Services.CommandSide.Domain.Products;

public sealed class Product : Entity
{
    public ProductName Name { get; private set; }

    public Money Price { get; private set; }

    public ProductQuantity Quantity { get; private set; }

    public ProductThumbnailUrl ThumbnailUrl { get; private set; }

    private Product()
    {
    }

    private Product(ProductName name, Money price, ProductQuantity quantity, ProductThumbnailUrl thumbnailUrl)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        ThumbnailUrl = thumbnailUrl;
    }

    public static Product Create(ProductName name, Money price, ProductQuantity quantity, ProductThumbnailUrl thumbnailUrl)
    {
        var product = new Product(name, price, quantity, thumbnailUrl);

        product.AddEvent(new NewProductCreatedEvent(product));

        return product;
    }

    public void Buy(ProductQuantity quantity) => Quantity -= quantity;
}