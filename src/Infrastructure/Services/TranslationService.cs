using System.ComponentModel.DataAnnotations;
using AutoFieldTranslationExperiment.Infrastructure;
using Azure;
using Azure.AI.Translation.Text;
using Domain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Translation = Domain.Translation;

namespace Infrastructure.Services;

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

    public async Task<List<Translation>> TranslateAsync(List<Translation> translations, Language sourceLanguage, List<Language> targetLanguages)
    {
        var source = sourceLanguage.Id == _languageInformation.CurrentBrowserLanguage.Id ? _languageInformation.CurrentBrowserLanguage : sourceLanguage;
        
        if (translations.Any(i => i.Language.Code != source.Code))
            throw new ValidationException("Source language code does not match the language code of the translations");
        
        var response = await _client.TranslateAsync(
            targetLanguages: targetLanguages.Select(t => t.Code), 
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
                    Value = newTranslation.Translations[j].Text,
                    Key = prevTranslation.Key,
                    ProductId = prevTranslation.ProductId
                });
            }
        }
        
        return translatedTexts;
    }

    public async Task AddAlternateTranslationsForEntityAsync(TranslatableEntity entity, List<Translation> translations)
    {
        if (translations.Any(i => i.LanguageId != _languageInformation.CurrentBrowserLanguage.Id))
            throw new ValidationException("Alternate translations must be in the current browser language");

        var source = _languageInformation.CurrentBrowserLanguage;
        var targets = _languageInformation.SupportedLanguages
            .Where(i => i.Id != source.Id)
            .ToList();

        if (targets.Count is 0)
            return;
        
        var translatedTexts = await TranslateAsync(
            translations: translations, 
            sourceLanguage: source, 
            targetLanguages: targets);
        
        entity.Translations.AddRange(translatedTexts);
        
        await _context.SaveChangesAsync();
    }

    public async Task AddTranslationForAllEntitiesAsync(Language? from, Language to)
    {
        // If no source language is provided, use the default language
        var source = from ?? await _context.Languages.FirstAsync(i => i.IsDefault);
        var entities = await _context.Translations
            .Where(i => i.LanguageId == source.Id)
            .Include(i => i.Language)
            .AsNoTracking()
            .ToListAsync();

        if (entities.Count is 0)
            return;
        
        var translations = await TranslateAsync(
            translations: entities, 
            sourceLanguage: source, 
            targetLanguages: [to]);

        if (translations.Count != entities.Count)
            throw new ValidationException("Number of translations returned does not match the number of translations sent");

        _context.Translations.UpdateRange(translations);
        await _context.SaveChangesAsync();
    }
}