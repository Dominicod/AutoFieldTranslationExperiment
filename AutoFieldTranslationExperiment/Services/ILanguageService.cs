using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Services;

public interface ILanguageService
{
    LanguageGet CurrentBrowserLanguage { get; set; }
    
    Task<IEnumerable<LanguageGet>> GetLanguagesAsync();

    Task<LanguageGet> GetLanguageByCode(string languageCode);

    Task<LanguageGet> AddLanguageAsync(LanguageCreate request);

    Task RemoveLanguageAsync(Guid id);
}