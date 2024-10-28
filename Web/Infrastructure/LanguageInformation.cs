using AutoFieldTranslationExperiment.DTOs.Language;

namespace AutoFieldTranslationExperiment.Infrastructure;

public class LanguageInformation
{
    public LanguageGet CurrentBrowserLanguage { get; set; } = null!;
    public List<LanguageGet> SupportedLanguages { get; set; } = [];
}