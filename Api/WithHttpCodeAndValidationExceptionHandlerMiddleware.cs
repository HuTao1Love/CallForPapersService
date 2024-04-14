using System.Net;
using Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api;

public class WithHttpCodeAndValidationExceptionHandlerMiddleware(
    RequestDelegate next,
    ProblemDetailsFactory problemDetailsFactory)
{
    public async Task Invoke(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            await next.Invoke(context);
        }
        catch (WithHttpCodeException e)
        {
            await SetProblem(context, e.Message, e.StatusCode);
        }
        catch (ValidationException e)
        {
            await SetProblem(context, e.Message, HttpStatusCode.BadRequest);
        }
    }

    private async Task SetProblem(HttpContext context, string message, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        var problem = problemDetailsFactory.CreateProblemDetails(context, (int)statusCode, detail: message);
        await context.Response.WriteAsJsonAsync(problem, context.RequestAborted);
    }
}