namespace Zeiss.Products.Domain.Events;

public sealed record ProductActivatedEvent(long ProductId) : DomainEvent;