using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Shared;

namespace AutoFieldTranslationExperiment.Models;

public class Translation : BaseEntity
{
    public required Guid LanguageId { get; set; }

    public Language Language { get; set; } = default!;

    public required string Key { get; set; } = string.Empty;

    public string? Value { get; set; }

    public TranslationGet MapToDto()
    {
        return new TranslationGet
        {
            Id = Id,
            LanguageCode = Language.Code,
            Value = Value,
            Key = Key
        };
    }
}