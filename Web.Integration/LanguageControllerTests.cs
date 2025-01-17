namespace Web.Test;

public class LanguageControllerTests
{
    public class GetAllMethod
    {
        public void Returns_AllLanguages()
        {
            
        }
        
        public void Returns_EmptyList_WhenNoLanguages()
        {
            
        }
    }
    
    public class AddMethod
    {
        public void Adds_Language()
        {
            
        }
        
        public void Returns_BadRequest_WhenLanguageAlreadyExists()
        {
            
        }
        
        public void Returns_BadRequest_WhenRequestIsInvalid()
        {
            
        }
    }
    
    public class SetDefaultMethod
    {
        public void Sets_DefaultLanguage()
        {
            
        }
        
        public void Returns_NotFound_WhenLanguageDoesNotExist()
        {
            
        }
        
        public void Returns_BadRequest_WhenLanguageIdIsInvalid()
        {
            
        }
        
        public void Returns_BadRequest_WhenLanguageIsAlreadyDefault()
        {
            
        }
    }
    
    public class RemoveMethod
    {
        public void Removes_Language()
        {
            
        }
        
        public void Returns_NotFound_WhenLanguageDoesNotExist()
        {
            
        }
        
        public void Returns_BadRequest_WhenLanguageIsDefault()
        {
            
        }
    }
}