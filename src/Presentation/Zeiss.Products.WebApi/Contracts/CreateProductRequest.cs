namespace Zeiss.Products.WebApi.Contracts;

public sealed class CreateProductRequest
{
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public required string? Description { get; set; }
    public required decimal Price { get; set; }
    public int? Quantity { get; set; }
}