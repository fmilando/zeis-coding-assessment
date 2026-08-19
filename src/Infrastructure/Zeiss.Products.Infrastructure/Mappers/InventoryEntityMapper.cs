using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Infrastructure.Database.Entities;

namespace Zeiss.Products.Infrastructure.Mappers;

internal static class InventoryEntityMapper
{
    public static InventoryEntity Map(Inventory entity) => new()
    {
        Id = entity.Id,
        ProductId = entity.ProductId,
        Quantity = entity.Quantity,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt ?? entity.CreatedAt
    };
    
    public static Inventory? Map(InventoryEntity? entity) => entity switch
    {
        null => null,
        _ => new Inventory(
            entity.Id, 
            entity.ProductId, 
            entity.Quantity!.Value, 
            entity.CreatedAt, 
            entity.UpdatedAt
        )
    };
}