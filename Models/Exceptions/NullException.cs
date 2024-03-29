using System.Net;

namespace Models.Exceptions;

public class NullException : WithHttpCodeException
{
    private const string Problem = "Value is null";

    public NullException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public NullException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public NullException()
    : base(Problem)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}