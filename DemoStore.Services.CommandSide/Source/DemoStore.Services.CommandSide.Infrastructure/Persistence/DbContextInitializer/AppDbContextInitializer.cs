using DemoStore.Services.CommandSide.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DemoStore.Services.CommandSide.Infrastructure.Persistence.DbContextInitializer;

internal sealed class AppDbContextInitializer : IAppDbContextInitializer
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger _logger;

    public AppDbContextInitializer(
        AppDbContext dbContext,
        ILogger<AppDbContextInitializer> logger
    )
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task MigrateAsync(CancellationToken token = default)
    {
        var dbContextName = nameof(AppDbContext);

        var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(token);
        var numberOfPendingMigrations = pendingMigrations.Count();

        _logger.LogInformation("{DbContext}'s Migration process started with {NumberOfMigrations} pending migrations."
            , dbContextName, numberOfPendingMigrations);

        try
        {
            var canConnect = await _dbContext.Database.CanConnectAsync(token);

            if (!canConnect)
            {
                var databaseCreator = _dbContext.GetService<IRelationalDatabaseCreator>();

                var databaseExists = await databaseCreator.ExistsAsync(token);

                if (!databaseExists)
                    await databaseCreator.CreateAsync(token);
            }


            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();


            await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync(token);
                await _dbContext.Database.MigrateAsync(token);
                await transaction.CommitAsync(token);
            });


            _logger.LogInformation("{DbContext}'s Migration process has been finished.", dbContextName);

        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "{DbContext}'s Migration process failed.", dbContextName);
            throw;
        }

    }


    public async Task SeedAsync(CancellationToken token = default)
    {
        var dbContextName = nameof(AppDbContext);

        try
        {
            _logger.LogInformation("{DbContext}'s seeding process started.", dbContextName);

            var flightsCount = await _dbContext.Products.CountAsync(token);

            if (flightsCount > 0)
            {
                _logger.LogWarning("Due to the already existing data, seeding the database is not possible!");
                return;
            }

            await TrySeedAsync(token);

            _logger.LogInformation("{DbContext}'s seeding process has been finished.", dbContextName);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "{DbContext}'s seeding process failed.", dbContextName);
            throw;
        }
    }


    private async Task TrySeedAsync(CancellationToken token)
    {
        var product1 = Product.Create(
            "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
            28.49m,
            1,
            "media/images/d1f0cf8a-ff87-4609-998d-d4939659776c.jpg"
            );

        var product2 = Product.Create(
            "Clean Code: A Handbook of Agile Software Craftsmanship",
            44.99m,
            3,
            "media/images/639a2fca-3817-48c1-915a-239d6da390bf.jpg"
            );

        var product3 = Product.Create(
            "The Clean Coder: A Code of Conduct for Professional Programmers",
            43.99m,
            5,
            "media/images/847beb1c-b0b4-4a52-8c01-1535c1d75cde.jpg"
            );

        var product4 = Product.Create(
            "Software Architecture with C# 10 and .NET 6",
            49.39m,
            2,
            "media/images/879d6cb6-5449-4816-83ca-21d1c4419a07.jpg"
            );

        var product5 = Product.Create(
            "C# 10 and .NET 6 – Modern Cross-Platform Development",
            44.99m,
            1,
            "media/images/3277fb7e-db4f-43c8-98c9-7b162b0bc128.jpg"
            );

        var product6 = Product.Create(
            "C# 10 in a Nutshell: The Definitive Reference",
            55.99m,
            7,
            "media/images/5d764be1-d7d5-483b-84e9-8ea621dda8a5.jpg"
            );

        var product7 = Product.Create(
            "Domain-Driven Design: Tackling Complexity in the Heart of Software",
            43.58m,
            0,
            "media/images/06e0108c-f96d-4617-9d56-1cac5e4d3e64.jpg"
            );

        var product8 = Product.Create(
            "Implementing Domain-Driven Design",
            55.68m,
            5,
            "media/images/d811856f-be79-4d4c-8cc0-dcdd88808964.jpg"
            );

        var product9 = Product.Create(
            "Patterns, Principles, and Practices of Domain-Driven Design",
            31.99m,
            5,
            "media/images/7e5adfbb-955b-4149-a2cf-34daa449fbfe.jpg"
            );

        var product10 = Product.Create(
            "Monolith to Microservices: Evolutionary Patterns to Transform Your Monolith",
            29.99m,
            2,
            "media/images/e789fdfc-b801-4f1d-ac2b-634d8f9d7d13.jpg"
            );

        var product11 = Product.Create(
            "Software Architecture: The Hard Parts: Modern Trade-Off Analyses for Distributed Architectures",
            25.39m,
            5,
            "media/images/663bf051-6603-471e-bb52-dc7c1c07a582.jpg"
            );

        var product12 = Product.Create(
            "Patterns of Enterprise Application Architecture",
            48.75m,
            3,
            "media/images/290ef4dd-f7be-4ea2-9160-7c313bfd6300.jpg"
            );



        var products = new List<Product>
        {
            product1, product2, product3, product4, product5, product6,
            product7, product8, product9, product10, product11, product12
        };

        await _dbContext.Products.AddRangeAsync(products, token);

        await _dbContext.SaveChangesAsync(token);
    }
}