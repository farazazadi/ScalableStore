using DemoStore.Services.CommandSide.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoStore.Services.CommandSide.Infrastructure.Persistence.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(product => product.Name)
            .HasConversion(productName => productName.ToString(), name => name)
            .IsRequired();

        builder.Property(product => product.Price)
            .HasConversion(money => (decimal)money, price => price)
            .IsRequired();

        builder.Property(product => product.Quantity)
            .HasConversion(productQuantity => (int)productQuantity, quantity => quantity)
            .IsRequired();

        builder.Property(product => product.ThumbnailUrl)
            .HasConversion(productThumbnailUrl => productThumbnailUrl.ToString(),
                url => url)
            .IsRequired();

        builder.Ignore(product => product.DomainEvents);
    }
}
