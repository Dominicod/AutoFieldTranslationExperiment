namespace AutoFieldTranslationExperiment.DTOs.Product;

public sealed record ProductGet
{
    public Guid Id { get; set; }

    public string? Name { get; set; } = string.Empty;
    
    public List<TranslationGet> NameTranslations { get; set; } = [];
}