using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Services;

public interface ILanguageService
{
    Task<IEnumerable<LanguageGet>> GetLanguagesAsync();

    Task<bool> LanguageExistsAsync(string languageCode);

    Task<Language> AddLanguageAsync(LanguageCreate request);

    Task RemoveLanguageAsync(Guid id);
}