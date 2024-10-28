namespace AutoFieldTranslationExperiment.DTOs.Translation;

public sealed record TranslationGet
{
    public Guid Id { get; init; }

    public string LanguageCode { get; init; } = string.Empty;

    public string Key { get; init; } = string.Empty;

    public string? Value { get; init; }
    
    public static TranslationGet Map(Domain.Translation translation)
    {
        return new TranslationGet
        {
            Id = translation.Id,
            LanguageCode = translation.Language.Code,
            Key = translation.Key,
            Value = translation.Value
        };
    }
}