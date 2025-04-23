using System.Net;
using System.Net.Http.Json;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Web.Test.LanguageController;

public abstract partial class LanguageControllerTest 
{
    public class Add(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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
}