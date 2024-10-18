using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.Data;
using AutoFieldTranslationExperiment.DTOs;
using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

internal sealed class ProductService(IApplicationDbContext context) : IProductService
{
    public async Task<ProductDto> GetProductAsync(Guid id)
    {
        var product = await context.Products
            .Include(i => i.NameTranslations)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        
        Guard.Against.NotFound("Product", product, nameof(product));
        
        return product.MapToDto();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        return await context.Products
            .Include(i => i.NameTranslations)
            .AsNoTracking()
            .Select(i => i.MapToDto())
            .ToListAsync();
    }

    public async Task<Product> CreateProductAsync(ProductDto request)
    {
        var product = new Product().FromDto(request);
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(ProductDto request)
    {
        var product = new Product().FromDto(request);
        context.Products.Update(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);

        Guard.Against.NotFound("Product", product, nameof(product));
        
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }
}