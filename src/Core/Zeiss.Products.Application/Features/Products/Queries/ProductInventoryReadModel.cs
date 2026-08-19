namespace Zeiss.Products.Application.Features.Products.Queries;

public sealed class ProductInventoryReadModel
{
    public required long ProductId { get; set; }
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public required string? Description { get; set; }
    public required decimal Price { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? UpdatedAt { get; set; }
    public required int? QuantityInStock { get; set; }
    public required DateTime? StockUpdatedAt { get; set; }
    public bool IsInventoryTracked => QuantityInStock is not null;
}