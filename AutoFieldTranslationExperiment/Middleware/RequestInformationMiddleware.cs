namespace AutoFieldTranslationExperiment.Middleware;

internal sealed class RequestInformationMiddleware(ILogger<RequestInformationMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
        
        await next(context);
    }
}