
using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cronometro = Stopwatch.StartNew();

        await next.Invoke(context);

        cronometro.Stop();

        if (cronometro.ElapsedMilliseconds > 4000)
            logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms.",
                context.Request.Method,
                context.Request.Path,
                cronometro.ElapsedMilliseconds);
    }
}
