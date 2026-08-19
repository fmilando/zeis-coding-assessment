namespace Zeiss.Products.Infrastructure.Database.Entities;

internal sealed class ProductEntity
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public required string? Description { get; set; }
    public required decimal Price { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? UpdatedAt { get; set; }
    public required DateTime? DeletedAt { get; set; }
}