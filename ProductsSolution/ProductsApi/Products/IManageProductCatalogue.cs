namespace ProductsApi.Products;

public interface IManageProductCatalogue
{
    Task<CreateProductResponse> AddProductAsync(CreateProductRequest request);
    Task<CreateProductResponse?> GetProductAsync(string slug);
}
