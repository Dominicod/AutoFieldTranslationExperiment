using AutoFieldTranslationExperiment.DTOs.Translation;

namespace AutoFieldTranslationExperiment.DTOs.Product;

public sealed record ProductGet
{
    public Guid Id { get; init; }

    public string? Name { get; init; } = string.Empty;

    public ProductTranslations Translations { get; init; } = new();

    public static ProductGet Map(Domain.Product product)
    {
        var names = product.Translations
            .Where(i => i.Key == nameof(Name))
            .Select(TranslationGet.Map)
            .ToList();

        return new ProductGet
        {
            Id = product.Id,
            Name = names.FirstOrDefault(i => i.LanguageCode == Thread.CurrentThread.CurrentCulture.Name)?.Value,
            Translations = new ProductTranslations
            {
                Names = names
            }
        };
    }
}