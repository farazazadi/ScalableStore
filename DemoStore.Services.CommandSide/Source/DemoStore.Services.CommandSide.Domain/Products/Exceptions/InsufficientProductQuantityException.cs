using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Domain.Products.Exceptions;
public sealed class InsufficientProductQuantityException : DomainException
{
    public override string Code => nameof(InsufficientProductQuantityException);

    public InsufficientProductQuantityException() : base("The requested product quantity is more than the stock quantity!")
    {
    }

    private InsufficientProductQuantityException(string message) : base(message)
    {
    }
}
