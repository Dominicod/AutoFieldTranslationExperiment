using AutoFieldTranslationExperiment.DTOs.Language;

namespace AutoFieldTranslationExperiment.Services;

public interface ILanguageService
{
    Task InitializeLanguageStateAsync(string browserLanguageCode);

    Task<LanguageGet> GetLanguageByCode(string languageCode);

    Task<Guid> AddLanguageAsync(LanguageCreate request);
    
    Task<bool> SetDefaultAsync(Guid languageId);

    Task RemoveLanguageAsync(Guid id);
}