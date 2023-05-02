using Marten.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Products;

public record CreateProductRequest
{
    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; init; } = string.Empty;
    public decimal Cost { get; init; }
    public SupplierInformation Supplier { get; init; } = new();
}

public record SupplierInformation
{
    public string Id { get; init; } = string.Empty;
    public string SKU { get; init; } = string.Empty;
}

public record CreateProductResponse
{
    [Identity]
    public string Slug { get; init; } = string.Empty;
    public ProductPricingInformation Pricing { get; init; } = new();
}

public record ProductPricingInformation
{
    public decimal Retail { get; set; }
    public ProductPricingWholeInformation Wholesale { get; init; } = new();
}

public record ProductPricingWholeInformation
{
    public decimal WholeSale { get; set;}
    public int MinimumPurchaseRequired { get; set; }
}