using ProductsApi.Adapters;
using ProductsApi.Products;

namespace ProductsApi.UnitTests;

public class DoingPricingCalculations
{
    [Theory]
    [InlineData(10.00, 5)]
    [InlineData(10.01, 5)]
    [InlineData(9.99, 10)]
    public void CalculatingMinimumPurchaseOnWholesale(decimal cost, int qty)
    {
        var request = new CreateProductRequest
        {
            Cost = cost
        };
        
        Assert.Equal(qty, PricingCalculations.CalculateMinimumPurchaseIsRequired(request));
    }

    [Fact]
    public void Banana()
    {
        var product = new CreateProductRequest
        {
            Cost = 10M
        };

        var info = new SupplierPricingInformationResponse
        {
            AllowWholesale = true
        };
        
        var answer = PricingCalculations.CalculateWholesalePrice(product, info);

        Assert.Equal(17.80M, answer);
    }
}
