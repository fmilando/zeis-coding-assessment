using System.Data;
using MediatR;
using Zeiss.Products.Application.Features.Products.Commands;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;

internal sealed class DecrementStockCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository products,
    IInventoryRepository inventories,
    IEventPublisher publisher
) : IRequestHandler<DecrementStockCommand, Result<ProductInventoryReadModel>>
{
    public async Task<Result<ProductInventoryReadModel>> Handle(
        DecrementStockCommand request, 
        CancellationToken cancellationToken)
    {
        await unitOfWork.StartAsync(IsolationLevel.RepeatableRead, cancellationToken);
        var product = await products.GetAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            await unitOfWork.DiscardAsync(cancellationToken);
            return new Error(
                ErrorCodes.Product.NotFound,
                $"Product {request.ProductId} not found");
        }

        var inventory = product.Inventory ?? await inventories.GetByProductIdAsync(request.ProductId, cancellationToken);
        
        if (inventory is null)
        {
            await unitOfWork.DiscardAsync(cancellationToken);
            return new Error(
                ErrorCodes.Inventory.NotTracked,
                "Inventory Not Tracked");
        }

        if (inventory.Quantity < request.Quantity)
        {
            await unitOfWork.DiscardAsync(cancellationToken);
            return new Error(
                ErrorCodes.Inventory.QuantityExceeded,
                "Insufficient inventory quantity"
            );
        }

        inventory.Decrement(request.Quantity);
        inventory = await inventories.UpdateAsync(inventory, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        await publisher.PublishAsync(inventory.Events.First(), cancellationToken);
        inventory.ClearEvents();

        return ProductInventoryReadModelMapper.Map(product, inventory);
    }
}