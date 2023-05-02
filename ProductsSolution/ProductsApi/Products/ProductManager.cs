namespace ProductsApi.Products;

public class ProductManager : IManageProductCatalogue
{
    private readonly IGenerateSlugs _slugGenerator;

    public ProductManager(IGenerateSlugs slugGenerator)
    {
        _slugGenerator = slugGenerator;
    }

    public async Task<CreateProductResponse> AddProductAsync(CreateProductRequest request)
    {
        var response = new CreateProductResponse
        {
            Slug = await _slugGenerator.GenerateSlugForAsync(request.Name),
            Pricing = new ProductPricingInformation
            {
                Retail = 42.23M,
                Wholesale = new ProductPricingWholeInformation
                {
                    WholeSale = 40.23M,
                    MinimumPurchaseRequired = 10
                }
            }
        };

        return response;
    }
}