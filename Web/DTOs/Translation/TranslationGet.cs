namespace AutoFieldTranslationExperiment.DTOs.Translation;

public sealed record TranslationGet
{
    public Guid Id { get; init; }

    public string LanguageCode { get; init; } = string.Empty;

    public string Key { get; init; } = string.Empty;

    public string? Value { get; init; }
}