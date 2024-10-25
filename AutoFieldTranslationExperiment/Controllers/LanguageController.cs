using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoFieldTranslationExperiment.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/[controller]")]
public class LanguageController(ILanguageService languageService) : ControllerBase
{
    [HttpGet]
    [Route("")]
    public Task<IResult> GetAll()
    {
        return Task.FromResult(Results.Ok(languageService.SupportedLanguages));
    }

    [HttpPost]
    [Route("")]
    public async Task<IResult> Add(LanguageCreate request)
    {
        var language = await languageService.AddLanguageAsync(request);
        return Results.Created(string.Empty, language);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IResult> Remove(Guid id)
    {
        await languageService.RemoveLanguageAsync(id);
        return Results.NoContent();
    }
}