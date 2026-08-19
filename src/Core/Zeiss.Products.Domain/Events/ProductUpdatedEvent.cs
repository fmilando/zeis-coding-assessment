using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Domain.Events;

public sealed record ProductUpdatedEvent(Product Product) : DomainEvent;