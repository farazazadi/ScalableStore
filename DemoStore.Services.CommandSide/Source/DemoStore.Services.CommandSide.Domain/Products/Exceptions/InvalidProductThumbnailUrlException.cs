using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Domain.Products.Exceptions;

public sealed class InvalidProductThumbnailUrlException : DomainException
{
    public override string Code => nameof(InvalidProductThumbnailUrlException);

    public InvalidProductThumbnailUrlException(string message) : base(message)
    {
    }
}