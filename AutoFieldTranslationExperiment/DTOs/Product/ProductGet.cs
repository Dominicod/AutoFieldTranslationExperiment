namespace AutoFieldTranslationExperiment.DTOs.Product;

public sealed record ProductGet
{
    public Guid Id { get; init; }

    public string? Name { get; init; } = string.Empty;
    
    public ProductTranslations Translations { get; init; } = new();
}