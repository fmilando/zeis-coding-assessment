namespace Zeiss.Products.Domain.Events;

public sealed record ProductDescriptionChangedEvent(
    long ProductId,
    string? OldDescription,
    string? NewDescription) : DomainEvent;