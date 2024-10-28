using Domain;
using Domain.Common;
using Translation = Domain.Translation;

namespace Infrastructure.Services;

public interface ITranslationService
{
    public Task<List<Translation>> TranslateAsync(List<Translation> translations, Language sourceLanguage, List<Language> targetLanguages);
    
    public Task AddAlternateTranslationsForEntityAsync(TranslatableEntity entity, List<Translation> translation);

    public Task AddTranslationForAllEntitiesAsync(Language? from, Language to);
}