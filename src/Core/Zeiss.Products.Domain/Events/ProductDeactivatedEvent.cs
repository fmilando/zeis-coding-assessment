namespace Zeiss.Products.Domain.Events;

public sealed record ProductDeactivatedEvent(long ProductId) : DomainEvent;