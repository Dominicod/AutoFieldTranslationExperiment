namespace AutoFieldTranslationExperiment.DTOs.Product;

public sealed record ProductUpdate
{
    public Guid Id { get; init; } = Guid.Empty;

    public string Name { get; init; } = string.Empty;
}