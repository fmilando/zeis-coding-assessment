namespace Zeiss.Products.WebApi.Contracts;

public sealed class UpdateProductRequest
{
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public required string? Description { get; set; }
    public required decimal Price { get; set; }
}