using Zeiss.Products.Domain.Events;

namespace Zeiss.Products.Domain.Entities;

public abstract record Entity<T>(T Id)
{
    private readonly List<DomainEvent> _events = new();
    protected void AddEvent(DomainEvent domainEvent) => _events.Add(domainEvent);

    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();
    public void ClearEvents() => _events.Clear();
}