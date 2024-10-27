namespace AutoFieldTranslationExperiment.DTOs.Translation;

public sealed record TranslationUpdate
{
    public Guid Id { get; init; }

    public string LanguageCode { get; init; } = string.Empty;

    public string? Value { get; init; }

    public string Key { get; init; } = string.Empty;
}