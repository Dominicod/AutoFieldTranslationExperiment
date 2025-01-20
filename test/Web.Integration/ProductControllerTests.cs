using Xunit;

namespace Web.Test;

public class ProductControllerTests
{
    public class GetAllMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Returns_AllProducts()
        {

        }

        [Fact]
        public void Returns_EmptyList_WhenNoProducts()
        {

        }
    }

    public class GetMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Returns_Product_WhenExists()
        {

        }

        [Fact]
        public void Returns_NotFound_WhenProductDoesNotExist()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenProductIdIsInvalid()
        {

        }
    }

    public class CreateMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Creates_Product()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenProductAlreadyExists()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenRequestIsInvalid()
        {

        }
    }

    public class UpdateMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Updates_Product()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenProductDoesNotExist()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenRequestIsInvalid()
        {

        }
    }

    public class DeleteMethod(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public void Deletes_Product()
        {

        }

        [Fact]
        public void Returns_NotFound_WhenProductDoesNotExist()
        {

        }

        [Fact]
        public void Returns_BadRequest_WhenProductIdIsInvalid()
        {

        }
    }
}
