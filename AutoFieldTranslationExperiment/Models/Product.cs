using AutoFieldTranslationExperiment.DTOs.Product;
using AutoFieldTranslationExperiment.Shared;

namespace AutoFieldTranslationExperiment.Models;

public sealed class Product : BaseEntity
{
    public List<Translation> Translations { get; set; } = [];
    
    public ProductGet MapToDto()
    {
        return new ProductGet
        {
            Id = Id,
            Name = Translations
                .Where(i => i.LanguageCode == Thread.CurrentThread.CurrentCulture.Name)
                .Select(i => i.Value)
                .FirstOrDefault(),
            Translations = new ProductTranslations
            {
                Names = Translations.Select(i => i.MapToDto()).ToList()
            }
        };
    }
}