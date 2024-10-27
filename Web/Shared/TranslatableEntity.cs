using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Shared;

public abstract class TranslatableEntity : BaseEntity
{
    public List<Translation> Translations { get; set; } = [];
}