namespace Zeiss.Products.Domain.Events;

public sealed record ProductDeletedEvent(long ProductId) : DomainEvent;