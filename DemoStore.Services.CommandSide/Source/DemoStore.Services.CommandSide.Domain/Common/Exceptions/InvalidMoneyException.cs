namespace DemoStore.Services.CommandSide.Domain.Common.Exceptions;
public sealed class InvalidMoneyException : DomainException
{
    public override string Code => nameof(InvalidMoneyException);

    public InvalidMoneyException(string message) : base(message)
    {
    }
}
