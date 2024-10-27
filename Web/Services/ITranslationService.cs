using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Models;
using AutoFieldTranslationExperiment.Shared;
using Azure.AI.Translation.Text;
using Translation = AutoFieldTranslationExperiment.Models.Translation;

namespace AutoFieldTranslationExperiment.Services;

public interface ITranslationService
{
    public Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync();
    
    public Task<List<Translation>> TranslateAsync(List<Translation> translations, Language sourceLanguage, List<Language> targetLanguages);
    
    public Task<bool> AddAlternateTranslationsAsync(TranslatableEntity entity, List<Translation> translation);
}