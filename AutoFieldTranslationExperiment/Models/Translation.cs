using AutoFieldTranslationExperiment.DTOs;
using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Shared;

namespace AutoFieldTranslationExperiment.Models;

public class Translation : BaseEntity
{
    public string LanguageCode { get; set; } = string.Empty;
    
    public string Value { get; set; } = string.Empty;
    
    public Guid? ProductId { get; set; }
    
    public Product? Product { get; set; }
    
    public TranslationGet MapToDto()
    {
        return new TranslationGet
        {
            Id = Id,
            LanguageCode = LanguageCode,
            Value = Value
        };
    }
}