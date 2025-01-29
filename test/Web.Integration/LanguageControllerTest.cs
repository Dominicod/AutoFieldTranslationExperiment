using Xunit;
using Domain;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Web.Test;

public class LanguageControllerTest
{
    public class GetAllMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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

    public class AddMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public async Task Adds_Language()
        {
            // Arrange
            var newLanguage = new Language { Code = "es-ES" };

            // Act
            var response = await Client.PostAsJsonAsync("/api/language", newLanguage);

            // Assert
            Assert.NotNull(response.Content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Returns_BadRequest_WhenLanguageAlreadyExists()
        {
            // Arrange
            Context.Languages.Add(new Language { Id = Guid.NewGuid(), Code = "es-ES" });
            await Context.SaveChangesAsync();
            var existingLanguage = new Language { Code = "es-ES" };

            // Add the language first
            await Client.PostAsJsonAsync("/api/language", existingLanguage);

            // Act
            var response = await Client.PostAsJsonAsync("/api/language", existingLanguage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Language with this code already exists", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Returns_BadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var invalidLanguage = new Language { Code = "" };

            // Act
            var response = await Client.PostAsJsonAsync("/api/language", invalidLanguage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Language code cannot be empty", await response.Content.ReadAsStringAsync());
        }
    }

    public class SetDefaultMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public async Task Sets_DefaultLanguage()
        {
            // Arrange
            var language = new Language { Id = Guid.NewGuid(), Code = "en-US" };
            Context.Languages.Add(language);
            await Context.SaveChangesAsync();

            // Act
            var response = await Client.PutAsJsonAsync($"/api/language/setdefault?languageId={language.Id}", new { });

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var updatedLanguage = await Context.Languages.FindAsync(language.Id);
            Assert.NotNull(updatedLanguage);
            Assert.True(updatedLanguage.IsDefault);
        }

        [Fact]
        public async Task Returns_NotFound_WhenLanguageDoesNotExist()
        {
            // Arrange
            var nonExistentLanguageId = Guid.NewGuid();

            // Act
            var response = await Client.GetAsync($"/api/language/{nonExistentLanguageId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Returns_BadRequest_WhenLanguageIsAlreadyDefault()
        {
            // Arrange
            var language = new Language { Id = Guid.NewGuid(), Code = "en-US", IsDefault = true };
            Context.Languages.Add(language);
            await Context.SaveChangesAsync();

            // Act
            var response = await Client.PutAsJsonAsync($"/api/language/setdefault?languageId={language.Id}", new { });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Language is already default", await response.Content.ReadAsStringAsync());
        }
    }

    public class RemoveMethod
    {
        [Fact]
        public void Removes_Language()
        {

        }

        [Fact]
        public void Returns_NotFound_WhenLanguageDoesNotExist()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenLanguageIsDefault()
        {

        }
    }
}
