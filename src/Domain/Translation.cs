using Domain.Common;

namespace Domain;

public class Translation : BaseEntity
{
    public required Guid LanguageId { get; set; }

    public Language Language { get; set; } = default!;

    public required string Key { get; set; } = string.Empty;

    public string? Value { get; set; }
    
    public Guid? ProductId { get; set; }
}