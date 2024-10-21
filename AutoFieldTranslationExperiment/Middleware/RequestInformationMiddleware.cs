using System.Globalization;
using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.Services;

namespace AutoFieldTranslationExperiment.Middleware;

internal sealed class RequestInformationMiddleware(ILogger<RequestInformationMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = context.Request;
        var acceptLanguageHeader = request.Headers.AcceptLanguage;
        
        // Fetch the first value from language header, if not found, default to en-US
        // The first should in theory be the default language of the browser
        var preferredBrowserLanguage = acceptLanguageHeader.ToString().Split(',').FirstOrDefault();
        
        if (string.IsNullOrWhiteSpace(preferredBrowserLanguage))
        {
            logger.LogWarning("No language found in Accept-Language header, defaulting to en-US");
            preferredBrowserLanguage = "en-US";
        }
        
        var languageService = context.RequestServices.GetRequiredService<ILanguageService>();
        var languageExists = await languageService.LanguageExistsAsync(preferredBrowserLanguage);
        
        if (!languageExists)
        {
            logger.LogError("Language {LanguageCode} not found in database", preferredBrowserLanguage);
            throw new NotFoundException("Language", nameof(preferredBrowserLanguage));
        }
        
        Thread.CurrentThread.CurrentCulture = new CultureInfo(preferredBrowserLanguage);
        
        logger.LogInformation("Request: {Method} {Path} Language: {LanguageCode}", context.Request.Method, context.Request.Path, preferredBrowserLanguage);
        
        await next(context);
    }
}