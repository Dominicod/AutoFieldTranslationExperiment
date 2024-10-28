using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Language;
using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Infrastructure;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using Azure;
using Azure.AI.Translation.Text;
using Domain;
using Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Translation = Domain.Translation;

namespace AutoFieldTranslationExperiment.Services;

public class TranslationService : ITranslationService
{
    private readonly TextTranslationClient _client;
    private readonly IApplicationDbContext _context;
    private readonly LanguageInformation _languageInformation;
    
    public TranslationService(IConfiguration configuration, IApplicationDbContext context, LanguageInformation languageInformation)
    {
        _context = context;
        _languageInformation = languageInformation;
        var credential = new AzureKeyCredential(configuration["Keys:Azure:AIService"] ?? throw new InvalidOperationException("Azure AIService key not found"));
        var region = configuration["AzureRegions:AIService"] ?? throw new InvalidOperationException("Azure AIService region not found");
        _client = new TextTranslationClient(credential, region);
    }

    public async Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync()
    {
        var languagesRes = await _client.GetLanguagesAsync(scope: "translation");
        var languages = languagesRes.Value.Translation;
        return languages.Select(l => new TranslationGetSupported(l.Key, l.Value.NativeName, l.Value.Name));
    }

    public async Task<List<Translation>> TranslateAsync(List<Translation> translations, Language sourceLanguage, List<Language> targetLanguages)
    {
        LanguageGet source;
        
        if (sourceLanguage.Id == _languageInformation.CurrentBrowserLanguage.Id)
            source = _languageInformation.CurrentBrowserLanguage;
        else
        {
            var language = await _context.Languages
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == sourceLanguage.Id);
            
            Guard.Against.NotFound("Source Language", language, nameof(source));
            
            source = LanguageGet.Map(language);
        }
        
        var targetIds = targetLanguages.Select(t => t.Id);
        var targets = await _context.Languages
            .AsNoTracking()
            .Where(l => targetIds.Contains(l.Id))
            .ToListAsync();
        
        if (targets.Count != targetLanguages.Count)
        {
            var missing = targetIds.Except(targets.Select(t => t.Id));
            throw new NotFoundException("Language Targets", $"One or more target languages not found: {string.Join(", ", missing)}");
        }
        
        if (translations.Any(i => i.Language.Code != source.Code))
            throw new ValidationException("Source language code does not match the language code of the translations");
        
        var response = await _client.TranslateAsync(
            targetLanguages: targets.Select(t => t.Code), 
            content: translations.Select(t => t.Value), 
            sourceLanguage: source.Code,
            textType: TextType.Plain,
            allowFallback: false);
        
        var translatedTexts = new List<Translation>();

        for (var i = 0; i < response.Value.Count; i++)
        {
            var newTranslation = response.Value[i];
            var prevTranslation = translations[i];
            
            for (var j = 0; j < newTranslation.Translations.Count; j++)
            {
                var language = targetLanguages[j];
                translatedTexts.Add(new Translation
                {
                    LanguageId = language.Id,
                    Language = language,
                    Value = newTranslation.Translations[j].Text,
                    Key = prevTranslation.Key
                });
            }
        }
        
        return translatedTexts;
    }

    public async Task AddAlternateTranslationsForEntityAsync(TranslatableEntity entity, List<Translation> translations)
    {
        if (translations.Any(i => i.LanguageId != _languageInformation.CurrentBrowserLanguage.Id))
            throw new ValidationException("Alternate translations must be in the current browser language");

        var source = new Language
        {
            Id = _languageInformation.CurrentBrowserLanguage.Id,
            Code = _languageInformation.CurrentBrowserLanguage.Code
        };
        var targets = _languageInformation.SupportedLanguages
            .Where(i => i.Id != source.Id)
            .Select(i => new Language
            {
                Id = i.Id,
                Code = i.Code
            }).ToList();

        if (targets.Count is 0)
            return;
        
        var translatedTexts = await TranslateAsync(
            translations: translations, 
            sourceLanguage: source, 
            targetLanguages: targets);
        
        if (translatedTexts.Count != translations.Count)
            throw new InvalidOperationException("Number of translations returned does not match the number of translations sent");
        
        entity.Translations.AddRange(translatedTexts);
        
        await _context.SaveChangesAsync();
    }

    public async Task TranslateAllEntitiesAsync(Language? from, Language to)
    {
        var entities = await _context.Translations
            .AsSplitQuery()
            .Where(i => i.LanguageId != to.Id)
            .ToListAsync();

        if (entities.Count is 0)
            return;

        // If no source language is provided, use the default language
        var source = from ?? await _context.Languages.FirstAsync(i => i.IsDefault);
        
        var translations = await TranslateAsync(
            translations: entities, 
            sourceLanguage: source, 
            targetLanguages: [to]);

        var test = "";
    }
}