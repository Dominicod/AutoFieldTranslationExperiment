using AutoFieldTranslationExperiment.DTOs.Translation;
using Azure.AI.Translation.Text;
using Translation = AutoFieldTranslationExperiment.Models.Translation;

namespace AutoFieldTranslationExperiment.Services;

public interface ITranslationService
{
    public Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync();
    
    public Task<IReadOnlyList<TranslatedTextItem>> BulkTranslateAsync(List<Translation> translations, Guid sourceLanguage, List<Guid> targetLanguages);
}