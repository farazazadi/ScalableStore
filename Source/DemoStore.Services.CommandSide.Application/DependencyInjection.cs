using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace DemoStore.Services.CommandSide.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        AddMappers(services);

        return services;
    }

    private static IServiceCollection AddMappers(IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Default.Settings.MapToConstructor = true;

        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

}