using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class ApplicationIsSubmittedException : WithHttpCodeException
{
    private const string Problem = "Application is submitted";

    public ApplicationIsSubmittedException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public ApplicationIsSubmittedException()
        : base(Problem)
    {
    }

    public ApplicationIsSubmittedException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}