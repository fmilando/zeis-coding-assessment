namespace Zeiss.Products.Domain.Events;

public sealed record InventoryOutOfStockEvent(
    long InventoryId,
    long ProductId
) : DomainEvent;