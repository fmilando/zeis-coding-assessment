namespace Zeiss.Products.Domain.Events;

public sealed record InventoryRestockedEvent(
    long InventoryId,
    long ProductId,
    long Quantity
) : DomainEvent;