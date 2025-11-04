using Microsoft.AspNetCore.Mvc;
using ResultPattern.Api.Extensions;
using ResultPattern.Core.Shared;
using ResultPattern.Service.Dto.ProductDto;
using ResultPattern.Service.Interfaces;

namespace ResultPattern.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] PaginationParams queryParams)
    {
        var result = await productService.GetAllProductsAsync(queryParams);
        return result.ToPagedResult();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await productService.GetProductByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var result = await productService.CreateProductAsync(request);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        var result = await productService.UpdateProductAsync(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await productService.DeleteProductAsync(id);
        return result.ToActionResult();
    }
}