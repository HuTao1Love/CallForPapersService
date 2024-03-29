using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class NotDefinedException : WithHttpCodeException
{
    private const string Problem = "All required fields must be defined";

    public NotDefinedException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public NotDefinedException()
        : base(Problem)
    {
    }

    public NotDefinedException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}