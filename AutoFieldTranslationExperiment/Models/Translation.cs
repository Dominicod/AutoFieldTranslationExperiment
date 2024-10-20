using AutoFieldTranslationExperiment.DTOs;
using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Shared;

namespace AutoFieldTranslationExperiment.Models;

public class Translation : BaseEntity
{
    public string LanguageCode { get; set; } = string.Empty;
    
    public string Key { get; set; } = string.Empty;
    
    public string? Value { get; set; }
    
    public TranslationGet MapToDto()
    {
        return new TranslationGet
        {
            Id = Id,
            LanguageCode = LanguageCode,
            Value = Value,
            Key = Key,
        };
    }
}