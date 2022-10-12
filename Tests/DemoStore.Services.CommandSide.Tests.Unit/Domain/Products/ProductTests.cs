using DemoStore.Services.CommandSide.Domain.Products;
using DemoStore.Services.CommandSide.Domain.Products.Events;

namespace DemoStore.Services.CommandSide.Tests.Unit.Domain.Products;

public class ProductTests
{
    [Fact]
    public void Product_ShouldHaveNewProductCreatedEvent_WhenItCreated()
    {
        // Given
        // When
        var product = Product.Create(
            "The Feast of the Goat",
            22.47m,
            5,
            "media/images/1.png"
        );

        // Then
        product.DomainEvents.Should()
            .HaveCount(1)
            .And
            .ContainSingle(domainEvent => domainEvent is NewProductCreatedEvent);
    }
}
