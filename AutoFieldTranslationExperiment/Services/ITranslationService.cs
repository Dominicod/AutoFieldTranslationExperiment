using AutoFieldTranslationExperiment.DTOs.Translation;

namespace AutoFieldTranslationExperiment.Services;

public interface ITranslationService
{
    public Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync();
    
    public Task BulkTranslateAsync(Guid sourceLanguage, Guid targetLanguage);
}