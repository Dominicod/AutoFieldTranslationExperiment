namespace Domain.Common;

public abstract class TranslatableEntity : BaseEntity
{
    public List<Translation> Translations { get; set; } = [];
}