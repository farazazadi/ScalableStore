using System.Globalization;
using DemoStore.Services.CommandSide.Domain.Products.Exceptions;

namespace DemoStore.Services.CommandSide.Domain.Products;

public sealed record ProductQuantity
{
    private readonly int _quantity;

    public ProductQuantity(int quantity)
    {
        if (quantity < 0)
            throw new InvalidProductQuantityException($"{nameof(quantity)} ({quantity}) can not be negative!");

        _quantity = quantity;
    }


    public static implicit operator int(ProductQuantity productQuantity) => productQuantity._quantity;
    public static implicit operator ProductQuantity(int productQuantity) => new ProductQuantity(productQuantity);

    public override string ToString() => _quantity.ToString(CultureInfo.InvariantCulture);
}