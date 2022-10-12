using DemoStore.Services.CommandSide.Domain.Products;
using DemoStore.Services.CommandSide.Domain.Products.Exceptions;

namespace DemoStore.Services.CommandSide.Tests.Unit.Domain.Products;
public class ProductQuantityTests
{
    public static TheoryData<int> NegativeInputs = new()
    {
        -1,
        -2001,
        -995999
    };

    [Theory, MemberData(nameof(NegativeInputs))]
    public void ProductQuantity_ShouldThrowInvalidProductQuantityException_WhenInputIsNegative(int productQuantity)
    {
        // Given
        // When
        var func = () => new ProductQuantity(productQuantity);

        // Then
        func.Should().ThrowExactly<InvalidProductQuantityException>()
            .And.Code.Should().Be(nameof(InvalidProductQuantityException));
    }
}
