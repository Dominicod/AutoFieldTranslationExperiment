using AutoFieldTranslationExperiment.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Services;

public class LanguageService(IApplicationDbContext context) : ILanguageService
{
    public async Task<bool> LanguageExistsAsync(string languageCode)
    {
        return await context.Languages.AnyAsync(l => l.Code == languageCode);
    }
}