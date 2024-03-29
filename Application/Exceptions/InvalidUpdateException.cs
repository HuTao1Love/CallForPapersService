using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class InvalidUpdateException : WithHttpCodeException
{
    private const string Problem = "Invalid update";

    public InvalidUpdateException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public InvalidUpdateException()
        : base(Problem)
    {
    }

    public InvalidUpdateException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}