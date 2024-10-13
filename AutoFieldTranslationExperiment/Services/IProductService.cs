using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Services;

public interface IProductService
{
    public Task<Product> GetProductAsync(int id);
    
    public Task<IEnumerable<Product>> GetProductsAsync();
}