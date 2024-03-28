namespace Application.Exceptions;

public abstract class WithHttpCodeException : Exception
{
    protected WithHttpCodeException(string message)
        : base(message)
    {
    }

    protected WithHttpCodeException()
    {
    }

    protected WithHttpCodeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public abstract int HttpCode { get; }
}