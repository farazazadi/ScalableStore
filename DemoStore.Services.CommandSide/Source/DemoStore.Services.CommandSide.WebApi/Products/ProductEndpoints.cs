using DemoStore.Services.CommandSide.Application.Products.Commands.BuyProduct;
using MediatR;

namespace DemoStore.Services.CommandSide.WebApi.Products;

internal static class ProductEndpoints
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapPut("api/products/{id}/buy", BuyProduct);

        return app;
    }

    private static async Task<IResult> BuyProduct(
        Guid id,
        BuyProductCommand command,
        ISender sender,
        CancellationToken token)
    {
        var buyProductResultDto = await sender.Send(command, token);

        return Results.Ok(buyProductResultDto);
    }
}
