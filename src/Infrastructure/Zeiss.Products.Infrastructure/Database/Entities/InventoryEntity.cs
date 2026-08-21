namespace Zeiss.Products.Infrastructure.Database.Entities;

internal sealed class InventoryEntity
{
    public int Id { get; set; }
    public required int ProductId { get; set; }
    public required int? Quantity { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? UpdatedAt { get; set; }
}