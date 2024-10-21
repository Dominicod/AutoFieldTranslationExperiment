using AutoFieldTranslationExperiment.DTOs.Product;
using AutoFieldTranslationExperiment.Shared;

namespace AutoFieldTranslationExperiment.Models;

public sealed class Product : BaseEntity
{
    public List<Translation> Translations { get; set; } = [];

    public ProductGet MapToDto()
    {
        var names = Translations
            .Where(i => i.Key == nameof(ProductGet.Name))
            .Select(i => i.MapToDto())
            .ToList();

        return new ProductGet
        {
            Id = Id,
            Name = names.FirstOrDefault(i => i.LanguageCode == Thread.CurrentThread.CurrentCulture.Name)?.Value,
            Translations = new ProductTranslations
            {
                Names = names
            }
        };
    }
}