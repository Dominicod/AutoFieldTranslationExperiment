namespace AutoFieldTranslationExperiment.DTOs.Product;

public sealed record ProductCreate
{
    public string Name { get; init; } = string.Empty;
}