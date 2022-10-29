using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Domain.Products.Exceptions;

public sealed class InvalidProductQuantityException : DomainException
{
    public override string Code => nameof(InvalidProductQuantityException);

    public InvalidProductQuantityException(string message) : base(message)
    {
    }
}