using Domain.Common;

namespace Domain;

public class Language : BaseEntity
{
    public required string Code { get; set; } = string.Empty;
}