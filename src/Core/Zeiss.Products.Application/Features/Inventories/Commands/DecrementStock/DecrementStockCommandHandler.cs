using Zeiss.Products.Application.Features.Products.Commands;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;

internal sealed class DecrementStockCommandHandler(
    IProductRepository products,
    IInventoryRepository inventories,
    IEventPublisher publisher
) : BaseCommandHandler<DecrementStockCommand, DecrementStockResult>(products)
{
    protected override async Task<Result<DecrementStockResult>> HandleAsync(
        DecrementStockCommand request, 
        Product product,
        CancellationToken cancellationToken)
    {
        await inventories.StartAsync(cancellationToken);
        var inventory = await inventories.GetAsync(request.ProductId, cancellationToken);
        
        if (inventory is null)
        {
            await inventories.DiscardAsync(cancellationToken);
            var error = new Error(ErrorCodes.InventoryNotTracked, "Inventory Not Tracked");
            return new Result<DecrementStockResult>([error]);
        }

        if (inventory.Quantity < request.Quantity)
        {
            await inventories.DiscardAsync(cancellationToken);
            var error = new Error(
                ErrorCodes.InsufficientQuantity,
                "Insufficient inventory quantity");
            
            return new Result<DecrementStockResult>([error]);
        }
        
        inventory.Decrement(request.Quantity);
        inventory = await inventories.UpdateAsync(inventory, cancellationToken);
        await inventories.CompleteAsync(cancellationToken);
        
        await publisher.PublishAsync(inventory.Events.First(), cancellationToken);
        inventory.ClearEvents();
        
        var model = ProductInventoryReadModelMapper.Map(product, inventory);
        var result = new DecrementStockResult(model);
        return new Result<DecrementStockResult>(result);
    }
}