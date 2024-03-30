using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class StringIsTooLongException : WithHttpCodeException
{
    private const string Problem = "String is too long";

    public StringIsTooLongException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public StringIsTooLongException()
        : base(Problem)
    {
    }

    public StringIsTooLongException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}