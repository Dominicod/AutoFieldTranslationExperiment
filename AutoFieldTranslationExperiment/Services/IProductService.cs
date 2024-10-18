using AutoFieldTranslationExperiment.DTOs;
using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Services;

public interface IProductService
{
    public Task<ProductDto> GetProductAsync(Guid id);
    
    public Task<IEnumerable<ProductDto>> GetProductsAsync();
    
    public Task<Product> CreateProductAsync(ProductDto product);
    
    public Task<Product> UpdateProductAsync(ProductDto product);
    
    public Task DeleteProductAsync(Guid id);
}