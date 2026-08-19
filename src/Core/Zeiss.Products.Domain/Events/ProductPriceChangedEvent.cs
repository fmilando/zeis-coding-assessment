namespace Zeiss.Products.Domain.Events;

public sealed record ProductPriceChangedEvent(
    long ProductId, 
    decimal OldPrice, 
    decimal NewPrice) : DomainEvent;