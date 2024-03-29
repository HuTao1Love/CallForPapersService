using System.Net;

namespace Models.Exceptions;

public class InvalidActivityException : WithHttpCodeException
{
    private const string Problem = "Invalid activity";
    public InvalidActivityException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public InvalidActivityException()
        : base(Problem)
    {
    }

    public InvalidActivityException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}