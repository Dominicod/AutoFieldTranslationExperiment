using AutoFieldTranslationExperiment.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoFieldTranslationExperiment.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetProductsAsync();
        
        return Ok(products);
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await productService.GetProductAsync(id);
        
        return Ok(product);
    }
}