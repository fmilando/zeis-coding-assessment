using System.Data;
using MediatR;
using Zeiss.Products.Application.Features.Products.Commands;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Domain.Events;

namespace Zeiss.Products.Application.Features.Inventories.Commands.AddToStock;

internal sealed class AddToStockCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository products,
    IInventoryRepository inventories,
    IEventPublisher publisher
) : IRequestHandler<AddToStockCommand, Result<ProductInventoryReadModel>>
{
    public async Task<Result<ProductInventoryReadModel>> Handle(
        AddToStockCommand request,
        CancellationToken cancellationToken)
    {
        await unitOfWork.StartAsync(IsolationLevel.RepeatableRead, cancellationToken);
        var product = await products.GetAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return new Error(ErrorCodes.Product.NotFound, "Product not found");
        }

        var inventory = product.Inventory ?? await inventories.GetByProductIdAsync(request.ProductId, cancellationToken);
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

        await unitOfWork.CompleteAsync(cancellationToken);
        await publisher.PublishAsync(@event, cancellationToken);
        inventory.ClearEvents();

        var model = ProductInventoryReadModelMapper.Map(product, inventory);

        return model;
    }
}