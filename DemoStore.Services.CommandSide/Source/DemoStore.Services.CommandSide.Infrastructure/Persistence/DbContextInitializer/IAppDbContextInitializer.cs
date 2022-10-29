namespace DemoStore.Services.CommandSide.Infrastructure.Persistence.DbContextInitializer;
public interface IAppDbContextInitializer
{
    Task MigrateAsync(CancellationToken token = default);
    Task SeedAsync(CancellationToken token = default);
}