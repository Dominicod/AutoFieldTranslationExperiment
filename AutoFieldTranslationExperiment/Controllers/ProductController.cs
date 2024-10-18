using Ardalis.GuardClauses;
using AutoFieldTranslationExperiment.DTOs;
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
        try
        {
            var product = await productService.GetProductAsync(id);
            return Results.Ok(product);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IResult> Create(ProductDto request)
    {
        var product = await productService.CreateProductAsync(request);
        
        return Results.Created($"/api/products/{product.Id}", product);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IResult> Update(ProductDto request)
    {
        try
        {
            await productService.UpdateProductAsync(request);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}