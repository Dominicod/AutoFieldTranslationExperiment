using AutoFieldTranslationExperiment.DTOs.Product;
using AutoFieldTranslationExperiment.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoFieldTranslationExperiment.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [Route("")]
    public async Task<IResult> GetAll()
    {
        var products = await productService.GetProductsAsync();
        return Results.Ok(products);
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IResult> Get(Guid id)
    {
        var product = await productService.GetProductAsync(id);
        return Results.Ok(product);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IResult> Create(ProductCreate request)
    {
        var product = await productService.CreateProductAsync(request);
        return Results.Created($"/api/products/{product.Id}", product);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IResult> Update(ProductUpdate request)
    {
        await productService.UpdateProductAsync(request);
        return Results.NoContent();
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IResult> Delete(Guid id)
    {
        await productService.DeleteProductAsync(id);
        return Results.NoContent();
    }
}