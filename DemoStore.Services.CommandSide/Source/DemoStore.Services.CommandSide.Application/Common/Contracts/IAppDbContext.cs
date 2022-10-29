using DemoStore.Services.CommandSide.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace DemoStore.Services.CommandSide.Application.Common.Contracts;
public interface IAppDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken token);
}