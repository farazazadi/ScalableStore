using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Domain.Products.Exceptions;

public sealed class InvalidProductNameException : DomainException
{
    public override string Code => nameof(InvalidProductNameException);

    public InvalidProductNameException(string message) : base(message)
    {
    }
}