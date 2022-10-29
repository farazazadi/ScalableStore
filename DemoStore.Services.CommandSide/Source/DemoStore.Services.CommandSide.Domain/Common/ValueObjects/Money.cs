using System.Globalization;
using DemoStore.Services.CommandSide.Domain.Common.Exceptions;

namespace DemoStore.Services.CommandSide.Domain.Common.ValueObjects;

public sealed record Money
{
    private readonly decimal _amount;

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new InvalidMoneyException($"Amount of money ({amount}) can not be negative!");

        _amount = amount;
    }


    public static implicit operator decimal(Money money) => money._amount;
    public static implicit operator Money(decimal amount) => new Money(amount);

    public override string ToString() => _amount.ToString(CultureInfo.InvariantCulture);
}
