using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Features.Products.Commands;

internal static class ProductInventoryReadModelMapper
{
    public static ProductInventoryReadModel Map(
        Product product, 
        Inventory inventory) => Map(product, inventory.Quantity, inventory.UpdatedAt ?? inventory.CreatedAt);
    
    public static ProductInventoryReadModel Map(Product product) => Map(product, null, null);
    
    private static ProductInventoryReadModel Map(
        Product product, 
        int? quantity, 
        DateTime? stockUpdatedAt) => new()
    {
        ProductId = product.Id,
        Name = product.Name,
        Sku = product.Sku,
        Description = product.Description,
        Price = product.Price,
        IsActive = product.IsActive,
        QuantityInStock = quantity,
        CreatedAt = product.CreatedAt,
        UpdatedAt = product.UpdatedAt,
        StockUpdatedAt = stockUpdatedAt
    };
}