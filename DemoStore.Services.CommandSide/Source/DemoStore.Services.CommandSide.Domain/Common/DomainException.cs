namespace DemoStore.Services.CommandSide.Domain.Common;

public abstract class DomainException : Exception
{
    public abstract string Code { get; }
    protected DomainException(string message) : base(message) { }
}