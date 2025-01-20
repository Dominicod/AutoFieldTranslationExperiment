using Xunit;

namespace Web.Test;

public class LanguageControllerTests
{
    public class GetAllMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Returns_AllLanguages()
        {
            // Arrange
            var response = Client.GetAsync("/api/language");

            // Act

            // Assert
        }

        [Fact]
        public void Returns_EmptyList_WhenNoLanguages()
        {

        }
    }

    public class AddMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Adds_Language()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenLanguageAlreadyExists()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenRequestIsInvalid()
        {

        }
    }

    public class SetDefaultMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Sets_DefaultLanguage()
        {

        }

        [Fact]
        public void Returns_NotFound_WhenLanguageDoesNotExist()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenLanguageIdIsInvalid()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenLanguageIsAlreadyDefault()
        {

        }
    }

    public class RemoveMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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
