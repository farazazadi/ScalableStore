using DemoStore.Services.CommandSide.Domain.Products.Exceptions;

namespace DemoStore.Services.CommandSide.Domain.Products;

public sealed record ProductName
{
    private readonly string _name;

    public ProductName(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new InvalidProductNameException($"{nameof(productName)} ({productName}) can not be null or white space!");


        var trimmedName = productName.Trim();

        if(trimmedName.Length is < 3 or > 100)
            throw new InvalidProductNameException($"Length of {nameof(productName)} ({productName}) should be between 3 and 50 characters!");

        _name = trimmedName;
    }


    public static implicit operator string(ProductName productName) => productName._name;
    public static implicit operator ProductName(string productName) => new ProductName(productName);

    public override string ToString() => _name;
}