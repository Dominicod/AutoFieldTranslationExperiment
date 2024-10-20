namespace AutoFieldTranslationExperiment.DTOs.Translation;

public sealed record TranslationCreate
{
    public string LanguageCode { get; init; } = string.Empty;

    public string? Value { get; init; }
}