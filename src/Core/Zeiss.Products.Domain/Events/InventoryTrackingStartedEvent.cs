namespace Zeiss.Products.Domain.Events;

public sealed record InventoryTrackingStartedEvent(
    long InventoryId,
    long ProductId,
    int Quantity
) : DomainEvent;