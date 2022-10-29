namespace DemoStore.Services.QuerySide.Products;

internal static class MappingExtensions
{
    internal static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto(
            product.ExternalId,
            product.Name,
            product.Price,
            product.Quantity,
            product.ThumbnailUrl
        );
    }

    internal static IList<ProductDto> ToProductDtoList(this IEnumerable<Product> products)
        => products.Select(ToProductDto).ToList();
}