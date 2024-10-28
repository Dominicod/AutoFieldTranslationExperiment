using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

public class LanguageService(IApplicationDbContext context) : ILanguageService
{
    public LanguageGet CurrentBrowserLanguage { get; set; } = null!;
    public List<LanguageGet> SupportedLanguages { get; set; } = [];
    
    public async Task InitializeLanguageStateAsync(string browserLanguageCode)
    {
        SupportedLanguages = await context.Languages
            .AsNoTracking()
            .Select(i => LanguageGet.Map(i))
            .ToListAsync();
        
        var browserLanguage = SupportedLanguages.FirstOrDefault(i => i.Code == browserLanguageCode);
        
        Guard.Against.NotFound("Language", browserLanguage, nameof(browserLanguage));
        
        CurrentBrowserLanguage = browserLanguage;
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
            Code = request.Code
        };

        await context.Languages.AddAsync(language);
        await context.SaveChangesAsync();

        return LanguageGet.Map(language);
    }

    public async Task RemoveLanguageAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ValidationException("Language Id cannot be empty");

        var language = await context.Languages.FindAsync(id);

        Guard.Against.NotFound("Language", language, nameof(language));

        context.Languages.Remove(language);
        await context.SaveChangesAsync();
    }
}