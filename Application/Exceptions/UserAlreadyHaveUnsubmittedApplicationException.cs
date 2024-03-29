using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class UserAlreadyHaveUnsubmittedApplicationException : WithHttpCodeException
{
    private const string Problem = "User already have unsubmitted application";

    public UserAlreadyHaveUnsubmittedApplicationException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public UserAlreadyHaveUnsubmittedApplicationException()
        : base(Problem)
    {
    }

    public UserAlreadyHaveUnsubmittedApplicationException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}