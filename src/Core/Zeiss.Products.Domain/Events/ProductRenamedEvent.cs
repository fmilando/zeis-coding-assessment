namespace Zeiss.Products.Domain.Events;

public record ProductRenamedEvent(
    long ProductId,
    string OldName,
    string NewName) : DomainEvent;