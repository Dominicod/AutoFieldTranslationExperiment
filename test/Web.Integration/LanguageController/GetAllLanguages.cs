using System.Net.Http.Json;
using Domain;
using Xunit;

namespace Web.Test.LanguageController;

public abstract partial class LanguageControllerTest
{
    public class GetAllLanguages(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public async Task Returns_AllLanguages()
        {
            // Arrange
            Context.Languages.Add(new Language { Id = Guid.NewGuid(), Code = "es-ES" });
            await Context.SaveChangesAsync();

            // Act
            var response = await Client.GetAsync("/api/language");

            // Assert
            response.EnsureSuccessStatusCode();
            var languages = await response.Content.ReadFromJsonAsync<List<Language>>();
            Assert.NotNull(languages);
            // Migrations add a default en-US language
            Assert.Equal(2, languages.Count);
        }

        [Fact]
        public async Task Returns_Seeded_enUS_Language_By_Default()
        {
            // Arrange
            // No languages added

            // Act
            var response = await Client.GetAsync("/api/language");

            // Assert
            response.EnsureSuccessStatusCode();
            var languages = await response.Content.ReadFromJsonAsync<List<Language>>();
            Assert.NotNull(languages);
            Assert.Single(languages);
        }
    }
}
