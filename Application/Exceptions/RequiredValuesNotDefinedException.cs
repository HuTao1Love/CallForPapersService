using System.Net;
using Models.Exceptions;

namespace Application.Exceptions;

public class RequiredValuesNotDefinedException : WithHttpCodeException
{
    private const string Problem = "All required fields must be defined";

    public RequiredValuesNotDefinedException(string message)
        : base($"{Problem}: {message}")
    {
    }

    public RequiredValuesNotDefinedException()
        : base(Problem)
    {
    }

    public RequiredValuesNotDefinedException(string message, Exception innerException)
        : base($"{Problem}: {message}", innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}