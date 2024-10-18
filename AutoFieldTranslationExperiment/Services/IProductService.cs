using AutoFieldTranslationExperiment.DTOs;
using AutoFieldTranslationExperiment.DTOs.Product;
using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Services;

public interface IProductService
{
    public Task<ProductGet> GetProductAsync(Guid id);
    
    public Task<IEnumerable<ProductGet>> GetProductsAsync();
    
    public Task<ProductGet> CreateProductAsync(ProductCreate product);
    
    public Task<ProductGet> UpdateProductAsync(ProductUpdate product);
    
    public Task DeleteProductAsync(Guid id);
}