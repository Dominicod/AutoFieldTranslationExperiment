using Xunit;

namespace Web.Test.LanguageController;

public abstract partial class LanguageControllerTest 
{
    public class SetDefault(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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
}