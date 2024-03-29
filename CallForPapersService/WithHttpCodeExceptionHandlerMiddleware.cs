using Models.Exceptions;
namespace CallForPapersService;

public class WithHttpCodeExceptionHandlerMiddleware(RequestDelegate next)
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
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)e.StatusCode;
            await context.Response.WriteAsJsonAsync(
                new
                    {
                        StatusCode = (int)e.StatusCode,
                        Message = e.Message,
                    },
                context.RequestAborted);
        }
    }
}