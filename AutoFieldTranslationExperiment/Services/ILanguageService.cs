using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Services;

public interface ILanguageService
{
    Task<bool> LanguageExistsAsync(string languageCode);
    
    Task<Language> AddLanguageAsync(string languageCode);
    
    Task RemoveLanguageAsync(Guid id);
}