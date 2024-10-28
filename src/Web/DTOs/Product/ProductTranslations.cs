using AutoFieldTranslationExperiment.DTOs.Translation;

namespace AutoFieldTranslationExperiment.DTOs.Product;

public sealed record ProductTranslations
{
    public ICollection<TranslationGet> Names { get; init; } = [];
}