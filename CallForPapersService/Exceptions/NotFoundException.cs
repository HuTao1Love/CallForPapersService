using System.Net;
using Models.Exceptions;

namespace CallForPapersService.Exceptions;

public class NotFoundException : WithHttpCodeException
{
    private const string Problem = "Not found";

    public NotFoundException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public NotFoundException()
        : base(Problem)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}