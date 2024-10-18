using AutoFieldTranslationExperiment.DTOs.Translation;

namespace AutoFieldTranslationExperiment.DTOs.Product;

public sealed record ProductGet
{
    public Guid Id { get; init; }

    public string? Name { get; init; } = string.Empty;
    
    public List<TranslationGet> NameTranslations { get; init; } = [];
}