namespace AutoFieldTranslationExperiment.Services;

public interface ILanguageService
{
    Task<bool> LanguageExistsAsync(string languageCode);
}