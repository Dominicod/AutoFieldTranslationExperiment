using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.Data;
using AutoFieldTranslationExperiment.DTOs.Product;
using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

internal sealed class ProductService(IApplicationDbContext context) : IProductService
{
    public async Task<ProductGet> GetProductAsync(Guid id)
    {
        var product = await context.Products
            .Include(i => i.Translations)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        
        Guard.Against.NotFound("Product", product, nameof(product));
        
        return product.MapToDto();
    }

    public async Task<IEnumerable<ProductGet>> GetProductsAsync()
    {
        return await context.Products
            .Include(i => i.Translations)
            .AsNoTracking()
            .Select(i => i.MapToDto())
            .ToListAsync();
    }

    public async Task<ProductGet> CreateProductAsync(ProductCreate request)
    {
        var product = new Product
        {
            Translations = [
                new Translation
                {
                    LanguageCode = Thread.CurrentThread.CurrentCulture.Name,
                    Value = request.Name,
                    Key = nameof(request.Name)
                }
            ]
        };
        
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product.MapToDto();
    }

    public async Task<ProductGet> UpdateProductAsync(ProductUpdate request)
    {
        var product = await context.Products
            .Include(i => i.Translations)
            .FirstOrDefaultAsync(i => i.Id == request.Id);
        
        Guard.Against.NotFound("Product", product, nameof(product));
        
        var currentTranslation = product.Translations
            .Where(i => i.Key == nameof(request.Name))
            .FirstOrDefault(i => i.LanguageCode == Thread.CurrentThread.CurrentCulture.Name);
        
        if (currentTranslation is not null)
            currentTranslation.Value = request.Name;
        else
            product.Translations.Add(new Translation
            {
                LanguageCode = Thread.CurrentThread.CurrentCulture.Name,
                Value = request.Name,
                Key = nameof(request.Name)
            });
        
        context.Products.Update(product);
        await context.SaveChangesAsync();
        return product.MapToDto();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);

        Guard.Against.NotFound("Product", product, nameof(product));
        
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }
}