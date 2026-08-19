using Zeiss.Products.Application.Features.Products.Commands;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Domain.Events;

namespace Zeiss.Products.Application.Features.Inventories.Commands.AddToStock;

internal sealed class AddToStockCommandHandler(
    IProductRepository products,
    IInventoryRepository inventories,
    IEventPublisher publisher
) : BaseCommandHandler<AddToStockCommand, AddToStockResult>(products)
{
    protected override async Task<Result<AddToStockResult>> HandleAsync(
        AddToStockCommand request,
        Product product,
        CancellationToken cancellationToken)
    {
        await inventories.StartAsync(cancellationToken);
        var inventory = await inventories.GetAsync(request.ProductId, cancellationToken);
        DomainEvent @event;

        if (inventory is null)
        {
            inventory = new Inventory(request.ProductId, request.Quantity);
            inventory = await inventories.AddAsync(inventory, cancellationToken);
            @event = new InventoryTrackingStartedEvent(
                inventory.Id,
                inventory.ProductId,
                inventory.Quantity);
        }
        else
        {
            inventory.Increment(request.Quantity);
            inventory = await inventories.UpdateAsync(inventory, cancellationToken);
            @event = inventory.Events.First();
        }

        await inventories.CompleteAsync(cancellationToken);

        await publisher.PublishAsync(@event, cancellationToken);
        inventory.ClearEvents();

        var model = ProductInventoryReadModelMapper.Map(product, inventory);
        var result = new AddToStockResult(model);
        return new Result<AddToStockResult>(result);
    }
}