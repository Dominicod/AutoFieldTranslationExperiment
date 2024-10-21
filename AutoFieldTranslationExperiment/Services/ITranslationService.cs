namespace AutoFieldTranslationExperiment.Services;

public interface ITranslationService
{
    public Task BulkTranslateAsync(Guid sourceLanguage, Guid targetLanguage);
}