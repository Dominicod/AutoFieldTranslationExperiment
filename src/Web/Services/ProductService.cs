using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Product;
using AutoFieldTranslationExperiment.Infrastructure;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using Domain;
using FluentValidation;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

internal sealed class ProductService(IApplicationDbContext context, ITranslationService translationService, LanguageInformation languageInformation) : IProductService
{
    public async Task<ProductGet> GetProductAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ValidationException("Product Id cannot be empty");

        var product = await context.Products
            .Include(i => i.Translations)
                .ThenInclude(i => i.Language)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        Guard.Against.NotFound("Product", product, nameof(product));

        return ProductGet.Map(product);
    }

    public async Task<IEnumerable<ProductGet>> GetProductsAsync()
    {
        return await context.Products
            .Include(i => i.Translations)
                .ThenInclude(i => i.Language)
            .AsSplitQuery()
            .AsNoTracking()
            .Select(i => ProductGet.Map(i))
            .ToListAsync();
    }

    public async Task<Guid> CreateProductAsync(ProductCreate request)
    {
        if (string.IsNullOrEmpty(request.Name))
            throw new ValidationException("Product Name cannot be empty");
        
        var translation = new Translation
        {
            LanguageId = languageInformation.CurrentBrowserLanguage.Id,
            Value = request.Name,
            Key = nameof(request.Name)
        };
        var product = new Product
        {
            Translations = [translation]
        };

        var transaction = await context.BeginTransactionAsync();

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        await translationService.AddAlternateTranslationsForEntityAsync(product, product.Translations);
        
        await transaction.CommitAsync();
        
        return product.Id;
    }

    public async Task<Guid> UpdateProductAsync(ProductUpdate request)
    {
        if (request.Id == Guid.Empty)
            throw new ValidationException("Product Id cannot be empty");

        if (string.IsNullOrEmpty(request.Name))
            throw new ValidationException("Product Name cannot be empty");

        var product = await context.Products
            .Include(i => i.Translations)
                .ThenInclude(i => i.Language)
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        Guard.Against.NotFound("Product", product, nameof(product));

        var currentTranslation = product.Translations
            .Where(i => i.Key == nameof(request.Name))
            .FirstOrDefault(i => i.Language.Id == languageInformation.CurrentBrowserLanguage.Id);

        if (currentTranslation is not null)
            currentTranslation.Value = request.Name;
        else
            product.Translations.Add(new Translation
            {
                LanguageId = languageInformation.CurrentBrowserLanguage.Id,
                Value = request.Name,
                Key = nameof(request.Name)
            });
        
        var transaction = await context.BeginTransactionAsync();

        context.Products.Update(product);
        await context.SaveChangesAsync();
        await translationService.AddAlternateTranslationsForEntityAsync(product, product.Translations
            .Where(i => i.LanguageId == languageInformation.CurrentBrowserLanguage.Id)
            .ToList());
        
        await transaction.CommitAsync();
        
        return product.Id;
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