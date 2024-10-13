namespace AutoFieldTranslationExperiment.DTOs;

public sealed record ProductDto
{
    public Guid Id { get; init; }
    
    public List<TranslationDto> Names { get; init; }
}