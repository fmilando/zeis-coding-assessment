using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Domain.Events;

public sealed record ProductCreatedEvent(Product Product) : DomainEvent;