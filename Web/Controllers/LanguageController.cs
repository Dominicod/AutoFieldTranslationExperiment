using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Infrastructure;
using AutoFieldTranslationExperiment.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoFieldTranslationExperiment.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/[controller]")]
public class LanguageController(ILanguageService languageService, LanguageInformation languageInformation) : ControllerBase
{
    [HttpGet]
    [Route("")]
    public Task<IResult> GetAll()
    {
        return Task.FromResult(Results.Ok(languageInformation.SupportedLanguages));
    }

    [HttpPost]
    [Route("")]
    public async Task<IResult> Add(LanguageCreate request)
    {
        var language = await languageService.AddLanguageAsync(request);
        return Results.Created(string.Empty, language);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IResult> SetDefault(Guid languageId)
    {
        await languageService.SetDefaultAsync(languageId);
        return Results.NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IResult> Remove(Guid id)
    {
        await languageService.RemoveLanguageAsync(id);
        return Results.NoContent();
    }
}