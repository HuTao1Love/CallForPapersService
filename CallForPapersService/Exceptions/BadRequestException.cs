using System.Net;
using Models.Exceptions;

namespace CallForPapersService.Exceptions;

public class BadRequestException : WithHttpCodeException
{
    private const string Problem = "Bad request";

    public BadRequestException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public BadRequestException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public BadRequestException()
        : base(Problem)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}