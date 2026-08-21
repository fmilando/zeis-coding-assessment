using Zeiss.Products.Domain.Events;
using Zeiss.Products.Domain.Exceptions;

namespace Zeiss.Products.Domain.Entities;

public sealed record Product : Entity<int>
{
    public string Name { get; private set; }
    public string Sku { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public Inventory? Inventory { get; private set; }

    public Product(
        int id,
        string name,
        string sku,
        string? description,
        decimal price,
        bool isActive,
        bool isDeleted,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        Inventory? inventory
    ) : base(id)
    {
        Name = EnsureName(name);
        Sku = EnsureSku(sku);
        Description = description;
        Price = EnsurePrice(price);
        IsActive = isActive;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
        Inventory = inventory;
    }

    public Product(
        string name,
        string sku,
        string? description,
        decimal price
    ) : this(0, name, sku, description, price, true, false, DateTime.UtcNow, null, null, null)
    {
        AddEvent(new ProductCreatedEvent(this));
    }

    public void SetName(string name)
    {
        if (IsNotChanged(Name, name))
        {
            return;
        }

        var oldName = Name;
        Name = EnsureName(name);
        var @event = new ProductRenamedEvent(Id, oldName, name);
        UpdatedAt = @event.OccurredOn;
        AddEvent(@event);
    }

    public void SetSku(string sku)
    {
        if (IsNotChanged(Sku, sku))
        {
            return;
        }

        var oldSku = Sku;
        Sku = EnsureSku(sku);
        var @event = new ProductSkuChangedEvent(Id, oldSku, sku);
        UpdatedAt = @event.OccurredOn;
        AddEvent(@event);
    }

    public void SetDescription(string? description)
    {
        if (IsNotChanged(Description, description))
        {
            return;
        }

        var oldDescription = Description ?? string.Empty;
        Description = description;
        var @event = new ProductDescriptionChangedEvent(Id, oldDescription, description);
        UpdatedAt = @event.OccurredOn;
        AddEvent(@event);
    }

    public void SetPrice(decimal price)
    {
        if (IsNotChanged(Price, price))
        {
            return;
        }

        var oldPrice = Price;
        Price = EnsurePrice(price);
        var @event = new ProductPriceChangedEvent(Id, oldPrice, price);
        UpdatedAt = @event.OccurredOn;
        AddEvent(@event);
    }

    public void SetInventory(Inventory inventory) => Inventory = inventory;
    
    public void Delete()
    {
        if (IsDeleted)
        {
            throw new DomainException("Cannot delete an already deleted Product");
        }

        IsActive = false;
        IsDeleted = true;
        var @event = new ProductDeletedEvent(Id);
        UpdatedAt = @event.OccurredOn;
        DeletedAt = @event.OccurredOn;
        AddEvent(@event);
    }

    private static string EnsureName(string name) => EnsureValue(name, nameof(Name));

    private static string EnsureSku(string sku) => EnsureValue(sku, nameof(Sku));

    private static string EnsureValue(string text, string propertyName) => string.IsNullOrWhiteSpace(text) switch
    {
        true => throw new DomainException($"Product {propertyName} cannot be null or empty"),
        _ => text
    };

    private static decimal EnsurePrice(decimal price) => price switch
    {
        <= 0 => throw new DomainException("Price must be greater than zero"),
        _ => price
    };

    private static bool IsNotChanged<T>(
        T? currentValue,
        T? newValue
    ) => string.Equals(
        currentValue?.ToString(),
        newValue?.ToString(),
        StringComparison.InvariantCultureIgnoreCase
    );
}