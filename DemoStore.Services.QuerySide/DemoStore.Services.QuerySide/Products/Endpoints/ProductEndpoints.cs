using DemoStore.Services.QuerySide.Products.Queries;
using MediatR;

namespace DemoStore.Services.QuerySide.Products.Endpoints;

internal static class ProductEndpoints
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("api/products", GetAllProducts);

        return app;
    }

    private static async Task<IResult> GetAllProducts(ISender sender, CancellationToken token)
    {
        var productDtoList = await sender.Send(new GetAllProductsQuery(), token);

        return Results.Ok(productDtoList);
    }
}
