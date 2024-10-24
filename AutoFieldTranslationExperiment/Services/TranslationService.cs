using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using AutoFieldTranslationExperiment.Models;
using Azure;
using Azure.AI.Translation.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Translation = AutoFieldTranslationExperiment.Models.Translation;

namespace AutoFieldTranslationExperiment.Services;

public class TranslationService : ITranslationService
{
    private readonly TextTranslationClient _client;
    private readonly IApplicationDbContext _context;
    private readonly ILanguageService _languageService;
    
    public TranslationService(IConfiguration configuration, IApplicationDbContext context, ILanguageService languageService)
    {
        _context = context;
        _languageService = languageService;
        var credential = new AzureKeyCredential(configuration["Keys:Azure:AIService"] ?? throw new InvalidOperationException("Azure AIService key not found"));
        _client = new TextTranslationClient(credential);
    }

    public async Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync()
    {
        var languagesRes = await _client.GetLanguagesAsync(scope: "translation");
        var languages = languagesRes.Value.Translation;
        return languages.Select(l => new TranslationGetSupported(l.Key, l.Value.NativeName, l.Value.Name));
    }

    public async Task<IReadOnlyList<TranslatedTextItem?>> TranslateAsync(List<Translation> translations, Guid sourceLanguage, List<Guid> targetLanguages)
    {
        LanguageGet source;
        
        if (sourceLanguage == _languageService.CurrentBrowserLanguage.Id)
            source = _languageService.CurrentBrowserLanguage;
        else
        {
            var language = await _context.Languages
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == sourceLanguage);
            
            Guard.Against.NotFound("Source Language", language, nameof(source));
            
            source = language.MapToDto();
        }
        
        var targets = await _context.Languages
            .AsNoTracking()
            .Where(l => targetLanguages.Contains(l.Id))
            .ToListAsync();
        
        if (targets.Count != targetLanguages.Count)
        {
            var missing = targetLanguages.Except(targets.Select(t => t.Id));
            throw new NotFoundException("Language Targets", $"One or more target languages not found: {string.Join(", ", missing)}");
        }
        
        if (translations.Any(i => i.Language.Code != source.Code))
            throw new ValidationException("Source language code does not match the language code of the translations");
        
        var response = await _client.TranslateAsync(
            targetLanguages: targets.Select(t => t.Code), 
            content: translations.Select(t => t.Value), 
            sourceLanguage: source.Code);
        return response.Value;
    }
}