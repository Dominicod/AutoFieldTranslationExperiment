using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Infrastructure;
using AutoFieldTranslationExperiment.Services;
using Infrastructure;
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
        return Task.FromResult(Results.Ok(languageInformation.SupportedLanguages.Select(LanguageGet.Map)));
    }

    [HttpPost]
    [Route("")]
    public async Task<IResult> Add(LanguageCreate request)
    {
        var id = await languageService.AddLanguageAsync(request);
        return Results.Created(string.Empty, id);
    }
    
    [HttpPut]
    [Route("[action]/{languageId:guid}")]
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