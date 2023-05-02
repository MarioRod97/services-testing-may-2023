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
        return StatusCode(201, response);
    }
}