using AutoFieldTranslationExperiment.Models;

namespace AutoFieldTranslationExperiment.Services;

internal sealed class ProductService : IProductService
{
    public Task<Product> GetProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }
}