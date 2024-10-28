using Domain;

namespace Infrastructure;

public class LanguageInformation
{
    public Language CurrentBrowserLanguage { get; set; } = null!;
    public List<Language> SupportedLanguages { get; set; } = [];
}