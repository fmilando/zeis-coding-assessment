namespace Zeiss.Products.Domain.Events;

public sealed record ProductSkuChangedEvent(
    long ProductId, 
    string OldSku, 
    string NewSku) : DomainEvent;