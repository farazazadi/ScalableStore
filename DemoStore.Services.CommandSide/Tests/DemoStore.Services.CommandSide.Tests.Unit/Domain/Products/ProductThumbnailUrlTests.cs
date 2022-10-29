using DemoStore.Services.CommandSide.Domain.Products.Exceptions;
using DemoStore.Services.CommandSide.Domain.Products;

namespace DemoStore.Services.CommandSide.Tests.Unit.Domain.Products;
public class ProductThumbnailUrlTests
{
    public static TheoryData<string> NullOrWhiteSpaceStrings = new()
    {
        null,
        string.Empty,
        " ",
        "   "
    };

    [Theory, MemberData(nameof(NullOrWhiteSpaceStrings))]
    public void ProductThumbnailUrl_ShouldThrowInvalidProductThumbnailUrlException_WhenInputIsNullOrWhiteSpace(string url)
    {
        // Given
        // When
        var func = () => new ProductThumbnailUrl(url);

        // Then
        func.Should().ThrowExactly<InvalidProductThumbnailUrlException>()
            .And.Code.Should().Be(nameof(InvalidProductThumbnailUrlException));
    }


    public static TheoryData<string> InvalidThumbnailFileExtensions = new()
    {
        ".bmp",
        ".gif",
        ".jif"
    };

    [Theory, MemberData(nameof(InvalidThumbnailFileExtensions))]
    public void ProductThumbnailUrl_ShouldThrowInvalidProductThumbnailUrlException_WhenUrlEndsWithInvalidExtension(string url)
    {
        // Given
        // When
        var func = () => new ProductThumbnailUrl(url);

        // Then
        func.Should().ThrowExactly<InvalidProductThumbnailUrlException>()
            .And.Code.Should().Be(nameof(InvalidProductThumbnailUrlException));
    }
}
