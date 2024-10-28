using AutoFieldTranslationExperiment.DTOs.Product;

namespace AutoFieldTranslationExperiment.Services;

public interface IProductService
{
    public Task<ProductGet> GetProductAsync(Guid id);

    public Task<IEnumerable<ProductGet>> GetProductsAsync();

    public Task<Guid> CreateProductAsync(ProductCreate product);

    public Task<Guid> UpdateProductAsync(ProductUpdate product);

    public Task DeleteProductAsync(Guid id);
}