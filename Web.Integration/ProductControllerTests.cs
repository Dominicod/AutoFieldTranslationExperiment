namespace Web.Test;

public class ProductControllerTests
{
    public class GetAllMethod
    {
        public void Returns_AllProducts()
        {
            
        }
        
        public void Returns_EmptyList_WhenNoProducts()
        {
            
        }
    }
    
    public class GetMethod
    {
        public void Returns_Product_WhenExists()
        {
            
        }
        
        public void Returns_NotFound_WhenProductDoesNotExist()
        {
            
        }
        
        public void Returns_BadRequest_WhenProductIdIsInvalid()
        {
            
        }
    }

    public class CreateMethod
    {
        public void Creates_Product()
        {
            
        }
        
        public void Returns_BadRequest_WhenProductAlreadyExists()
        {
            
        }
        
        public void Returns_BadRequest_WhenRequestIsInvalid()
        {
            
        }
    }

    public class UpdateMethod
    {
        public void Updates_Product()
        {
            
        }
        
        public void Returns_BadRequest_WhenProductDoesNotExist()
        {
            
        }
        
        public void Returns_BadRequest_WhenRequestIsInvalid()
        {
            
        }
    }
    
    public class DeleteMethod
    {
        public void Deletes_Product()
        {
            
        }
        
        public void Returns_NotFound_WhenProductDoesNotExist()
        {
            
        }
        
        public void Returns_BadRequest_WhenProductIdIsInvalid()
        {
            
        }
    }
}