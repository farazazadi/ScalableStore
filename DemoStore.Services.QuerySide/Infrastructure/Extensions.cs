using System.Text.Json;
using DemoStore.Services.QuerySide.Common;
using DemoStore.Services.QuerySide.Products.IntegrationEvents;

namespace DemoStore.Services.QuerySide.Infrastructure;

internal static class Extensions
{
    public static dynamic ToIntegrationEvent(this string input)
    {
        var integrationEvent = JsonSerializer.Deserialize<IntegrationEvent>(input);

        return integrationEvent?.Type switch
        {
            nameof(NewProductCreatedIntegrationEvent) =>
                JsonSerializer.Deserialize<NewProductCreatedIntegrationEvent>(input),

            nameof(ProductBoughtIntegrationEvent) =>
                JsonSerializer.Deserialize<ProductBoughtIntegrationEvent>(input),

            _ => null
        };
    }
}
