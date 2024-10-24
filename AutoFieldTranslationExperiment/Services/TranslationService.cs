using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs.Translation;
using AutoFieldTranslationExperiment.Infrastructure.Data;
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
    
    public TranslationService(IConfiguration configuration, IApplicationDbContext context)
    {
        _context = context;
        var credential = new AzureKeyCredential(configuration["Keys:Azure:AIService"] ?? throw new InvalidOperationException("Azure AIService key not found"));
        _client = new TextTranslationClient(credential);
    }

    public async Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync()
    {
        var languagesRes = await _client.GetLanguagesAsync(scope: "translation");
        var languages = languagesRes.Value.Translation;
        return languages.Select(l => new TranslationGetSupported(l.Key, l.Value.NativeName, l.Value.Name));
    }

    public async Task<IReadOnlyList<TranslatedTextItem>> BulkTranslateAsync(List<Translation> translations, Guid sourceLanguage, List<Guid> targetLanguages)
    {
        var source = await _context.Languages
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == sourceLanguage);
        var targets = await _context.Languages
            .AsNoTracking()
            .Where(l => targetLanguages.Contains(l.Id))
            .ToListAsync();

        Guard.Against.NotFound("Source Language", source, nameof(source));
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