using System.Net;

namespace Application.Exceptions;

public class WithHttpCodeException : Exception
{
    public WithHttpCodeException(string? message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public WithHttpCodeException(string? message)
        : base(message)
    {
    }

    public WithHttpCodeException()
    {
    }

    public WithHttpCodeException(string? message, Exception innerException)
        : base(message, innerException)
    {
    }

    public HttpStatusCode StatusCode { get; }

    public static WithHttpCodeException NewBadRequest(string message) => new(message, HttpStatusCode.BadRequest);

    public static WithHttpCodeException NewNotFound(string message) => new(message, HttpStatusCode.NotFound);
}