using Xunit;

namespace Web.Test;

public class ProductControllerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    //https://xunit.net/docs/shared-context#class-fixture
    public class GetAllMethod
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
    
    public class GetMethod
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

    public class CreateMethod
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

    public class UpdateMethod
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
    
    public class DeleteMethod
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