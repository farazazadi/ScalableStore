namespace DemoStore.Services.QuerySide.Products;

internal record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity,
    string ThumbnailUrl
);