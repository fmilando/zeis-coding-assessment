using Zeiss.Products.Domain.Events;
using Zeiss.Products.Domain.Exceptions;

namespace Zeiss.Products.Domain.Entities;

public sealed record Inventory : Entity<int>
{
    public int ProductId { get; }
    public int Quantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Inventory(
        int id,
        int productId,
        int quantity,
        DateTime createdAt,
        DateTime? updatedAt) : base(id)
    {
        ProductId = productId;
        Quantity = EnsureQuantity(quantity);
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Inventory(int productId, int quantity)
        : this(0, productId, quantity, DateTime.UtcNow, null)
    { }

    public void Decrement(int quantity)
    {
        if (quantity is 0)
        {
            return;
        }

        if (quantity < 0)
        {
            throw new DomainException(nameof(quantity), "Quantity must be greater than 0");
        }

        if (Quantity < quantity)
        {
            throw new DomainException(nameof(quantity), "Not sufficient inventory quantity to decrement");
        }

        var oldQuantity = Quantity;
        Quantity -= EnsureQuantity(quantity);

        DomainEvent @event = Quantity switch
        {
            0 => new InventoryOutOfStockEvent(Id, ProductId),
            _ => new InventoryChangedEvent(Id, ProductId, oldQuantity, Quantity)
        };

        UpdatedAt = @event.OccurredOn;
        AddEvent(@event);
    }

    public void Increment(int quantity)
    {
        if (quantity is 0)
        {
            return;
        }

        if (quantity < 0)
        {
            throw new DomainException(nameof(quantity), "Quantity must be greater than 0");
        }

        var oldQuantity = Quantity;
        Quantity += EnsureQuantity(quantity);

        DomainEvent @event = oldQuantity switch
        {
            0 => new InventoryRestockedEvent(Id, ProductId, quantity),
            _ => new InventoryChangedEvent(Id, ProductId, oldQuantity, Quantity)
        };

        UpdatedAt = @event.OccurredOn;
        AddEvent(@event);
    }

    private static int EnsureQuantity(int quantity) => quantity switch
    {
        < 0 => throw new DomainException(nameof(quantity), "Quantity must be zero or greater"),
        _ => quantity
    };
}