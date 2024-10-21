using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Shared;

namespace AutoFieldTranslationExperiment.Models;

public class Language : BaseEntity
{
    public required string Code { get; set; } = string.Empty;

    public LanguageGet MapToDto()
    {
        return new LanguageGet(Id, Code);
    }
}