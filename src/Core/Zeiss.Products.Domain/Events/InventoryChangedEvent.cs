namespace Zeiss.Products.Domain.Events;

public sealed record InventoryChangedEvent(
    long InventoryId,
    long ProductId,
    int OldQuantity,
    int NewQuantity) : DomainEvent;