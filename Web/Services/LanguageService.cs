using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Infrastructure;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

public class LanguageService(IApplicationDbContext context, ITranslationService translationService, LanguageInformation languageInformation) : ILanguageService
{
    public async Task InitializeLanguageStateAsync(string browserLanguageCode)
    {
        languageInformation.SupportedLanguages = await context.Languages
            .AsNoTracking()
            .Select(i => LanguageGet.Map(i))
            .ToListAsync();
        
        var browserLanguage = languageInformation.SupportedLanguages.FirstOrDefault(i => i.Code == browserLanguageCode);
        
        Guard.Against.NotFound("Language", browserLanguage, nameof(browserLanguage));
        
        languageInformation.CurrentBrowserLanguage = browserLanguage;
    }

    public async Task<LanguageGet> GetLanguageByCode(string languageCode)
    {
        if (string.IsNullOrEmpty(languageCode))
            throw new ValidationException("Language code cannot be empty");

        var language = await context.Languages
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Code == languageCode);
        
        Guard.Against.NotFound("Language", language, nameof(language));
        
        return LanguageGet.Map(language);
    }

    public async Task<LanguageGet> AddLanguageAsync(LanguageCreate request)
    {
        if (string.IsNullOrEmpty(request.Code))
            throw new ValidationException("Language code cannot be empty");

        var language = new Language
        {
            Code = request.Code,
            IsDefault = false
        };
        
        var defaultExists = await context.Languages
            .AsNoTracking()
            .AnyAsync(i => i.IsDefault);
        
        if (!defaultExists)
            language.IsDefault = true;

        await context.Languages.AddAsync(language);
        await context.SaveChangesAsync();

        await translationService.TranslateAllEntitiesAsync(null, to: language);

        return LanguageGet.Map(language);
    }

    public async Task<bool> SetDefaultAsync(Guid languageId)
    {
        var language = await context.Languages.FindAsync(languageId);
        
        Guard.Against.NotFound("Language", language, nameof(language));
        
        if (language.IsDefault)
            throw new ValidationException("Language is already default");
        
        var currentDefaultLanguage = await context.Languages.FirstOrDefaultAsync(i => i.IsDefault);
        
        if (currentDefaultLanguage is not null)
        {
            currentDefaultLanguage.IsDefault = false;
            context.Languages.Update(currentDefaultLanguage);
        }
        
        language.IsDefault = true;
        context.Languages.Update(language);
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task RemoveLanguageAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ValidationException("Language Id cannot be empty");

        var language = await context.Languages.FindAsync(id);

        Guard.Against.NotFound("Language", language, nameof(language));
        
        if (language.IsDefault)
            throw new ValidationException("Cannot remove default language, set another language as default first");

        context.Languages.Remove(language);
        await context.SaveChangesAsync();
    }
}