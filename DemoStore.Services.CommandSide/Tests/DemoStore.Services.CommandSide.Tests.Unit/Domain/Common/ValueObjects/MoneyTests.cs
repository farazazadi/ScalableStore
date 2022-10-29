using DemoStore.Services.CommandSide.Domain.Common.Exceptions;
using DemoStore.Services.CommandSide.Domain.Common.ValueObjects;

namespace DemoStore.Services.CommandSide.Tests.Unit.Domain.Common.ValueObjects;
public class MoneyTests
{
    public static TheoryData<decimal> NegativeInputs = new()
    {
        -1,
        -55.50m,
        -1000,
        -9999999
    };

    [Theory, MemberData(nameof(NegativeInputs))]
    public void Money_ShouldThrowInvalidMoneyException_WhenInputIsNegative(decimal amount)
    {
        // Given
        // When
        var func = () => new Money(amount);

        // Then
        func.Should().ThrowExactly<InvalidMoneyException>()
            .And.Code.Should().Be(nameof(InvalidMoneyException));
    }
}
