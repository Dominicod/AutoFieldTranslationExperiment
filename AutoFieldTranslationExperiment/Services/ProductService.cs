using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Product;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using AutoFieldTranslationExperiment.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

internal sealed class ProductService(IApplicationDbContext context, ITranslationService translationService, ILanguageService languageService) : IProductService
{
    public async Task<ProductGet> GetProductAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ValidationException("Product Id cannot be empty");

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
        if (string.IsNullOrEmpty(request.Name))
            throw new ValidationException("Product Name cannot be empty");

        var product = new Product
        {
            Translations =
            [
                new Translation
                {
                    LanguageId = languageService.CurrentBrowserLanguage.Id,
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
        if (request.Id == Guid.Empty)
            throw new ValidationException("Product Id cannot be empty");

        if (string.IsNullOrEmpty(request.Name))
            throw new ValidationException("Product Name cannot be empty");

        var product = await context.Products
            .Include(i => i.Translations)
            .ThenInclude(i => i.Language)
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        Guard.Against.NotFound("Product", product, nameof(product));

        var currentTranslation = product.Translations
            .Where(i => i.Key == nameof(request.Name))
            .FirstOrDefault(i => i.Language.Id == languageService.CurrentBrowserLanguage.Id);

        if (currentTranslation is not null)
            currentTranslation.Value = request.Name;
        else
            product.Translations.Add(new Translation
            {
                LanguageId = languageService.CurrentBrowserLanguage.Id,
                Value = request.Name,
                Key = nameof(request.Name)
            });

        context.Products.Update(product);
        await context.SaveChangesAsync();
        return product.MapToDto();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ValidationException("Product Id cannot be empty");

        var product = await context.Products.FindAsync(id);

        Guard.Against.NotFound("Product", product, nameof(product));

        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }
}