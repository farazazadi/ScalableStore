using DemoStore.Services.QuerySide.Infrastructure.MongoDb;
using DemoStore.Services.QuerySide.Infrastructure.RabbitMq;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using MediatR;
using System.Reflection;

namespace DemoStore.Services.QuerySide.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        AddMongoDb(services, configuration);

        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));

        services.AddHostedService<RabbitMqSubscriber>();

        return services;
    }

    private static void AddMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbOptions = new MongoDbOptions();
        configuration.Bind(MongoDbOptions.SectionName, mongoDbOptions);
        services.AddSingleton(Options.Create(mongoDbOptions));

        var mongoClient = new MongoClient(mongoDbOptions.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbOptions.DatabaseName);

        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddTransient(typeof(IMongoDatabase), _ => mongoDatabase);

        RegisterConventions();
    }

    private static void RegisterConventions()
    {
        BsonSerializer.RegisterSerializer(
            typeof(decimal),
            new DecimalSerializer(BsonType.Decimal128)
            );

        BsonSerializer.RegisterSerializer(
            typeof(Guid),
             new GuidSerializer(BsonType.String)
        );

        var conventionPack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true),
            new CamelCaseElementNameConvention(),
            new EnumRepresentationConvention(BsonType.String)
        };

        ConventionRegistry.Register("DemoStore", conventionPack, _ => true);
    }
}
