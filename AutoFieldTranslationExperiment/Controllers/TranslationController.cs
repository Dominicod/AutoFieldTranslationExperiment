using AutoFieldTranslationExperiment.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoFieldTranslationExperiment.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/[controller]")]
public class TranslationController(ITranslationService translationService) : ControllerBase
{
    [HttpGet]
    [Route("/supported-languages")]
    public async Task<IResult> GetAll()
    {
        var supportedLanguages = await translationService.GetSupportedLanguagesAsync();
        return Results.Ok(supportedLanguages);
    }
}