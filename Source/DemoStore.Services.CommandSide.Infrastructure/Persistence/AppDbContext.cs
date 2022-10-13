using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DemoStore.Services.CommandSide.Application.Common.Contracts;
using DemoStore.Services.CommandSide.Domain.Products;

namespace DemoStore.Services.CommandSide.Infrastructure.Persistence;
internal sealed class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Product> Products { get; init; }

    private readonly IEnumerable<ISaveChangesInterceptor> _saveChangesInterceptors;

    public AppDbContext(DbContextOptions<AppDbContext> options, IEnumerable<ISaveChangesInterceptor> saveChangesInterceptors)
        : base(options)
    {
        _saveChangesInterceptors = saveChangesInterceptors;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_saveChangesInterceptors);

        base.OnConfiguring(optionsBuilder);
    }
}