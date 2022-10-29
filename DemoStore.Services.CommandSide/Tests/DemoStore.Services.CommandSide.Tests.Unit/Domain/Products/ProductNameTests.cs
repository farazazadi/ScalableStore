using DemoStore.Services.CommandSide.Domain.Products;
using DemoStore.Services.CommandSide.Domain.Products.Exceptions;

namespace DemoStore.Services.CommandSide.Tests.Unit.Domain.Products;
public class ProductNameTests
{
    public static TheoryData<string> NullOrWhiteSpaceStrings = new()
    {
        null,
        string.Empty,
        " ",
        "   "
    };

    [Theory, MemberData(nameof(NullOrWhiteSpaceStrings))]
    public void ProductName_ShouldThrowInvalidProductNameException_WhenInputIsNullOrWhiteSpace(string name)
    {
        // Given
        // When
        var func = () => new ProductName(name);

        // Then
        func.Should().ThrowExactly<InvalidProductNameException>()
            .And.Code.Should().Be(nameof(InvalidProductNameException));
    }


    public static TheoryData<string> ProductsWithInvalidLength = new()
    {
        "a",
        "ab",
        "mT4DAxRHumFohPX4U2yidymQVUqL0GG8aNu7UyOZRbmUFb6ltgmT4DAxRHumFohPX4U2yidymQVUqL0GG8aNu7UyOZRbmUFb6ltgA"
    };

    [Theory, MemberData(nameof(ProductsWithInvalidLength))]
    public void ProductName_ShouldThrowInvalidProductNameException_WhenLengthOfInputIsNotBetween3and100(string name)
    {
        // Given
        // When
        var func = () => new ProductName(name);

        // Then
        func.Should().ThrowExactly<InvalidProductNameException>()
            .And.Code.Should().Be(nameof(InvalidProductNameException));
    }

}
