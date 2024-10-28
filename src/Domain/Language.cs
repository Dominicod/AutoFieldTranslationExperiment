using Domain.Common;

namespace Domain;

public class Language : BaseEntity
{
    public required string Code { get; set; } = string.Empty;
    
    public bool IsDefault { get; set; }

    public ICollection<Translation> Translations { get; set; } = [];
}