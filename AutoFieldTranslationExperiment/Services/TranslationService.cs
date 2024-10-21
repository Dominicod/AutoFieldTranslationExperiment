using AutoFieldTranslationExperiment.DTOs.Translation;
using Azure;
using Azure.AI.Translation.Text;

namespace AutoFieldTranslationExperiment.Services;

public class TranslationService : ITranslationService
{
    private readonly TextTranslationClient _client;
    
    public TranslationService(IConfiguration configuration)
    {
        var credential = new AzureKeyCredential(configuration["Keys:Azure:AIService"] ?? throw new InvalidOperationException("Azure AIService key not found"));
        _client = new TextTranslationClient(credential);
    }

    public async Task<IEnumerable<TranslationGetSupported>> GetSupportedLanguagesAsync()
    {
        var languagesRes = await _client.GetLanguagesAsync();
        var languages = languagesRes.Value.Translation;
        return languages.Select(l => new TranslationGetSupported(l.Key, l.Value.NativeName, l.Value.Name));
    }

    public Task BulkTranslateAsync(Guid sourceLanguage, Guid targetLanguage)
    {
        throw new NotImplementedException();
        
        //// Choose target language
        // Response<GetLanguagesResult> languagesResponse = await client.GetLanguagesAsync(scope:"translation").ConfigureAwait(false);
        // GetLanguagesResult languages = languagesResponse.Value;
        // Console.WriteLine($"{languages.Translation.Count} languages available.\n(See https://learn.microsoft.com/azure/ai-services/translator/language-support#translation)");
        // Console.WriteLine("Enter a target language code for translation (for example, 'en'):");
        // string targetLanguage = "xx";
        // bool languageSupported = false;
        // while (!languageSupported)
        // {
        //     targetLanguage = Console.ReadLine();
        //     if (languages.Translation.ContainsKey(targetLanguage))
        //     {
        //         languageSupported = true;
        //     }
        //     else
        //     {
        //         Console.WriteLine($"{targetLanguage} is not a supported language.");
        //     }
        // 
        // }
        
        //// Translate text
        // string inputText = "";
        // while (inputText.ToLower() != "quit")
        // {
        //     Console.WriteLine("Enter text to translate ('quit' to exit)");
        //     inputText = Console.ReadLine();
        //     if (inputText.ToLower() != "quit")
        //     {
        //         Response<IReadOnlyList<TranslatedTextItem>> translationResponse = await client.TranslateAsync(targetLanguage, inputText).ConfigureAwait(false);
        //         IReadOnlyList<TranslatedTextItem> translations = translationResponse.Value;
        //         TranslatedTextItem translation = translations[0];
        //         string sourceLanguage = translation?.DetectedLanguage?.Language;
        //         Console.WriteLine($"'{inputText}' translated from {sourceLanguage} to {translation?.Translations[0].To} as '{translation?.Translations?[0]?.Text}'.");
        //     }
        // } 
    }
}