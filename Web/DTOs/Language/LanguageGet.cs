namespace AutoFieldTranslationExperiment.DTOs.Language;

public sealed record LanguageGet(Guid Id, string Code, bool IsDefault)
{
    public static LanguageGet Map(Domain.Language language)
    {
        return new LanguageGet(language.Id, language.Code, language.IsDefault);
    }
}