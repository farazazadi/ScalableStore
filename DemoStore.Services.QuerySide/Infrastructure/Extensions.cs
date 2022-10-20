using System.Text.Json;
using DemoStore.Services.QuerySide.Common;

namespace DemoStore.Services.QuerySide.Infrastructure;

internal static class Extensions
{
    public static dynamic ToIntegrationEvent(this string input)
    {
        var integrationEvent = JsonSerializer.Deserialize<IntegrationEvent>(input);

        return integrationEvent?.Type switch
        {
            _ => null
        };
    }
}
