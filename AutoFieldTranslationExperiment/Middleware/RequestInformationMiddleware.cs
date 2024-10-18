using System.Globalization;

namespace AutoFieldTranslationExperiment.Middleware;

internal sealed class RequestInformationMiddleware(ILogger<RequestInformationMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = context.Request;
        var acceptLanguageHeader = request.Headers.AcceptLanguage;
        
        // Fetch the first value from language header, if not found, default to en-US
        // The first should in theory be the default language of the browser
        var preferredBrowserLanguage = acceptLanguageHeader.ToString().Split(',').FirstOrDefault() ?? "en-US";
        Thread.CurrentThread.CurrentCulture = new CultureInfo(preferredBrowserLanguage);
        
        logger.LogInformation("Request: {Method} {Path} Language: {LanguageCode}", context.Request.Method, context.Request.Path, preferredBrowserLanguage);
        
        await next(context);
    }
}