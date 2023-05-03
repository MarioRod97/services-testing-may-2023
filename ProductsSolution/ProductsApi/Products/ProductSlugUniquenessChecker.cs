namespace ProductsApi.Products;

public class ProductSlugUniquenessChecker : ICheckForUniqueValues
{
    public async Task<bool> IsUniqueAsync(string attempt)
    {
        return true;
    }
}
