using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Products;

[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IManageProductCatalogue _productCatalogue;

    public ProductsController(IManageProductCatalogue productCatalogue)
    {
        _productCatalogue = productCatalogue;
    }

    [HttpPost("/products")]
    public async Task<ActionResult<CreateProductResponse>> AddProduct([FromBody] CreateProductRequest request)
    {
        CreateProductResponse response = await _productCatalogue.AddProductAsync(request);
        return CreatedAtAction(nameof(ProductsController.GetProductBySlug), new { slug = response.Slug }, response);
    }

    [HttpGet("/products/{slug}")]
    public async Task<ActionResult<CreateProductResponse>> GetProductBySlug(string slug)
    {
        CreateProductResponse? response = await _productCatalogue.GetProductAsync(slug);

        if(response == null)
        {
            return NotFound();
        } else
        {
            return Ok(response);
        }
    }
}