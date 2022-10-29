using DemoStore.Services.CommandSide.Application.Common.Contracts;
using DemoStore.Services.CommandSide.Infrastructure.Common.Contracts;
using DemoStore.Services.CommandSide.Infrastructure.Persistence;
using DemoStore.Services.CommandSide.Infrastructure.Persistence.DbContextInitializer;
using DemoStore.Services.CommandSide.Infrastructure.Persistence.Interceptors;
using DemoStore.Services.CommandSide.Infrastructure.Services;
using DemoStore.Services.CommandSide.Infrastructure.Services.RabbitMq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoStore.Services.CommandSide.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        AddDbContext(services, configuration);

        services
            .AddScoped<IEventDispatcher, EventDispatcherService>()
            .AddSingleton<IMessageBrokerPublisher, RabbitMqPublisher>()
            .Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));

        return services;
    }


    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<AppDbContext>(
            configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            );

        services
            .AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>())
            .AddScoped<DbContext>(provider => provider.GetRequiredService<AppDbContext>())
            .AddScoped<IAppDbContextInitializer, AppDbContextInitializer>()
            .AddScoped<ISaveChangesInterceptor, EventDispatchingInterceptor>();
    }
}