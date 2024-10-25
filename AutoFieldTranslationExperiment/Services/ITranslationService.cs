using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Models;
using Azure.AI.Translation.Text;
using Translation = AutoFieldTranslationExperiment.Models.Translation;

namespace AutoFieldTranslationExperiment.Services;

public interface ITranslationService
{
    public Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync();
    
    public Task<IReadOnlyList<TranslatedTextItem?>> TranslateAsync(List<Translation> translations, Language sourceLanguage, List<Language> targetLanguages);
    
    public Task<bool> AddAlternateTranslationsAsync(List<Translation> translation);
}