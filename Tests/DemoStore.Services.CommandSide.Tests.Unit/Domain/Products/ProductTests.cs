using DemoStore.Services.CommandSide.Domain.Products;
using DemoStore.Services.CommandSide.Domain.Products.Events;
using DemoStore.Services.CommandSide.Domain.Products.Exceptions;

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


    [Fact]
    public void Product_ShouldActAsExpected_WhenBuyMethodCalled()
    {
        // Given
        var product = Product.Create(
            "The Feast of the Goat",
            22.47m,
            5,
            "media/images/1.png"
        );

        // When
        product.Buy(2);

        // Then
        var expected = new ProductQuantity(3);
        product.Quantity.Should().Be(expected);
    }

    [Fact]
    public void BuyMethod_ShouldThrowInsufficientProductQuantityException_WhenRequestedProductQuantityIsMoreThanQuantityInStock()
    {
        // Given
        var product = Product.Create(
            "The Feast of the Goat",
            22.47m,
            5,
            "media/images/1.png"
        );

        // When
        var action = () => product.Buy(6);

        // Then
        action.Should().ThrowExactly<InsufficientProductQuantityException>()
            .And.Code.Should().Be(nameof(InsufficientProductQuantityException));
    }

    [Fact]
    public void Product_ShouldHaveProductBoughtEvent_WhenBuyMethodCalled()
    {
        // Given
        var product = Product.Create(
            "The Feast of the Goat",
            22.47m,
            5,
            "media/images/1.png"
        );

        // When
        product.Buy(3);

        // Then
        product.DomainEvents.Should()
            .ContainSingle(domainEvent => domainEvent is ProductBoughtEvent);

    }
}
