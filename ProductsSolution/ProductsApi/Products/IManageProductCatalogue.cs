namespace ProductsApi.Products;

public interface IManageProductCatalogue
{
    Task<CreateProductResponse> AddProductAsync(CreateProductRequest request);
}
