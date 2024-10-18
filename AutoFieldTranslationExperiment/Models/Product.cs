using AutoFieldTranslationExperiment.DTOs;
using AutoFieldTranslationExperiment.Shared;

namespace AutoFieldTranslationExperiment.Models;

public sealed class Product : BaseEntity
{
    public List<Translation> NameTranslations { get; set; } = [];
    
    public ProductDto MapToDto()
    {
        return new ProductDto
        {
            Id = Id,
            Names = NameTranslations.Select(i => i.MapToDto()).ToList()
        };
    }
    
    public Product FromDto(ProductDto dto)
    {
        Id = dto.Id;
        NameTranslations = dto.Names.Select(i => new Translation().FromDto(i)).ToList();
        
        return this;
    }
}