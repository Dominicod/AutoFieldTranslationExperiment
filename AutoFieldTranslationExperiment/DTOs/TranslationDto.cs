namespace AutoFieldTranslationExperiment.DTOs;

public sealed record TranslationDto
{
    public Guid Id { get; init; } = Guid.Empty;
    
    public string LanguageCode { get; init; } = string.Empty;
    
    public string Value { get; init; } = string.Empty;
}