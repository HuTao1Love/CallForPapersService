using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class AuthorMustBeDefinedException : WithHttpCodeException
{
    private const string Problem = "Author must be defined";

    public AuthorMustBeDefinedException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public AuthorMustBeDefinedException()
        : base(Problem)
    {
    }

    public AuthorMustBeDefinedException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}