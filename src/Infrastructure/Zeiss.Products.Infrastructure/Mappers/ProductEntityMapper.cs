using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Infrastructure.Database.Entities;

namespace Zeiss.Products.Infrastructure.Mappers;

internal static class ProductEntityMapper
{
    public static ProductEntity Map(Product entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Sku = entity.Sku,
        Description = entity.Description,
        Price = entity.Price,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        DeletedAt = entity.DeletedAt
    };
    
    public static Product? Map(ProductEntity? entity) => entity switch
    {
        null => null,
        _ => new Product(
            entity.Id,
            entity.Name, 
            entity.Sku, 
            entity.Description,
            entity.Price, 
            entity.IsActive,
            entity.IsDeleted,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt
        )
    };
}