using Marten;

namespace ProductsApi.Products;

public class ProductManager : IManageProductCatalogue
{
    private readonly IGenerateSlugs _slugGenerator;
    private readonly IDocumentSession _session;
    private readonly IManagePricing _pricingManager;

    public ProductManager(IGenerateSlugs slugGenerator, IDocumentSession session, IManagePricing pricing)
    {
        _slugGenerator = slugGenerator;
        _session = session;
        _pricingManager = pricing;
    }

    public async Task<CreateProductResponse> AddProductAsync(CreateProductRequest request)
    {
        var response = new CreateProductResponse
        {
            Slug = await _slugGenerator.GenerateSlugForAsync(request.Name),
            Pricing = await _pricingManager.GetPricingInformationForAsync(request)
        };

        _session.Insert(response);
        await _session.SaveChangesAsync();
        return response;
    }

    public async Task<CreateProductResponse?> GetProductAsync(string slug)
    {
        var response = await _session.Query<CreateProductResponse>().Where(p => p.Slug == slug).SingleOrDefaultAsync();
        return response;
    }

    public async Task<IList<CreateProductResponse>> GetAllAsync()
    {
        return await _session.Query<CreateProductResponse>().ToListAsync() as IList<CreateProductResponse>;
    }
}