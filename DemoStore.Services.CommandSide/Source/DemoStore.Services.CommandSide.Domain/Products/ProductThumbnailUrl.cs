using DemoStore.Services.CommandSide.Domain.Products.Exceptions;

namespace DemoStore.Services.CommandSide.Domain.Products;

public sealed record ProductThumbnailUrl
{
    private readonly string _url;

    public ProductThumbnailUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new InvalidProductThumbnailUrlException($"{nameof(url)} ({url}) can not be null or white space!");


        var trimmedUrl = url.Trim();

        var fileExtension = Path.GetExtension(trimmedUrl).ToLowerInvariant();


        if (fileExtension is not (".jpg" or ".png"))
            throw new InvalidProductThumbnailUrlException("Thumbnail should be jpg or png!");

        _url = trimmedUrl;
    }


    public static implicit operator string(ProductThumbnailUrl productThumbnailUrl) => productThumbnailUrl._url;
    public static implicit operator ProductThumbnailUrl(string url) => new ProductThumbnailUrl(url);

    public override string ToString() => _url;
}