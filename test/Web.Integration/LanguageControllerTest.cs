using Xunit;
using Domain;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;

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
            Context.Languages.Add(new Language { Id = Guid.NewGuid(), Code = "ja" });
            await Context.SaveChangesAsync();
            var existingLanguage = new Language { Code = "ja" };

            // Add the language first
            await Client.PostAsJsonAsync("/api/language", existingLanguage);

            // Act
            var response = await Client.PostAsJsonAsync("/api/language", existingLanguage);

            // Assert
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("https://tools.ietf.org/html/rfc7231#section-6.5.1", problemDetails!.Type);
            Assert.Equal("Bad Request", problemDetails.Title);
            Assert.Equal("Language with this code already exists", problemDetails.Detail);
        }

        [Fact]
        public async Task Returns_BadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var invalidLanguage = new Language { Code = "" };

            // Act
            var response = await Client.PostAsJsonAsync("/api/language", invalidLanguage);

            // Assert
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("https://tools.ietf.org/html/rfc7231#section-6.5.1", problemDetails!.Type);
            Assert.Equal("Bad Request", problemDetails.Title);
            Assert.Equal("Language code cannot be empty", problemDetails.Detail);
        }
    }

    public class SetDefaultMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Sets_DefaultLanguage()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Returns_NotFound_WhenLanguageDoesNotExist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Returns_BadRequest_WhenLanguageIsAlreadyDefault()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    public class RemoveMethod
    {
        [Fact]
        public void Removes_Language()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Returns_NotFound_WhenLanguageDoesNotExist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Returns_BadRequest_WhenLanguageIsDefault()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
