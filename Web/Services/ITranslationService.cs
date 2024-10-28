using AutoFieldTranslationExperiment.DTOs.Translation;
using Azure.AI.Translation.Text;
using Domain;
using Domain.Common;
using Translation = Domain.Translation;

namespace AutoFieldTranslationExperiment.Services;

public interface ITranslationService
{
    public Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync();
    
    public Task<List<Translation>> TranslateAsync(List<Translation> translations, Language sourceLanguage, List<Language> targetLanguages);
    
    public Task AddAlternateTranslationsForEntityAsync(TranslatableEntity entity, List<Translation> translation);

    public Task TranslateAllEntitiesAsync(Language? from, Language to);
}