using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using AutoFieldTranslationExperiment.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

public class LanguageService(IApplicationDbContext context) : ILanguageService
{
    public async Task<IEnumerable<LanguageGet>> GetLanguagesAsync()
    {
        return await context.Languages
            .Select(i => i.MapToDto())
            .ToListAsync();
    }

    public async Task<bool> LanguageExistsAsync(string languageCode)
    {
        if (string.IsNullOrEmpty(languageCode))
            throw new ValidationException("Language code cannot be empty");

        return await context.Languages.AnyAsync(l => l.Code == languageCode);
    }

    public async Task<Language> AddLanguageAsync(LanguageCreate request)
    {
        if (string.IsNullOrEmpty(request.Code))
            throw new ValidationException("Language code cannot be empty");

        var language = new Language
        {
            Code = request.Code
        };

        await context.Languages.AddAsync(language);
        await context.SaveChangesAsync();

        return language;
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