namespace DemoStore.Services.CommandSide.Application.Common.Exceptions;
public abstract class ApplicationException : Exception
{
    public abstract string Code { get; }
    protected ApplicationException(string message) : base(message) { }
}