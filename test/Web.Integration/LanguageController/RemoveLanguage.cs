using Xunit;

namespace Web.Test.LanguageController;

public abstract partial class LanguageControllerTest 
{
    public class Remove
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
