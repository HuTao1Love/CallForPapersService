using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class AnyFieldMustBeDefinedException : WithHttpCodeException
{
    private const string Problem = "At least one field except author must be set";

    public AnyFieldMustBeDefinedException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public AnyFieldMustBeDefinedException()
        : base(Problem)
    {
    }

    public AnyFieldMustBeDefinedException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}