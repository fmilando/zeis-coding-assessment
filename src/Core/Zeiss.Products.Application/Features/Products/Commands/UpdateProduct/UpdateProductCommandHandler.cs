using System.Data;
using MediatR;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Events;

namespace Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository products,
    IInventoryRepository inventories,
    IEventPublisher publisher) : IRequestHandler<UpdateProductCommand, Result<ProductInventoryReadModel>>
{
    public async Task<Result<ProductInventoryReadModel>> Handle(
        UpdateProductCommand request, 
        CancellationToken cancellationToken)
    {
        await unitOfWork.StartAsync(IsolationLevel.RepeatableRead, cancellationToken);
        var product = await products.GetAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            await unitOfWork.DiscardAsync(cancellationToken);
            return new Error(
                ErrorCodes.Product.NotFound,
                $"Product ${request.ProductId} not found"
            );
        }
        
        product.SetName(request.Name);
        product.SetDescription(request.Description);
        product.SetPrice(request.Price);
        product.SetSku(request.Sku);

        var inventory = product.Inventory ?? await inventories.GetByProductIdAsync(request.ProductId, cancellationToken);
        var productUpdated = product.Events.Count > 0;
        
        if (productUpdated)
        {
            product = await products.UpdateAsync(product, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            await PublishEventsAsync(product.Events, cancellationToken);
            product.ClearEvents();
        }

        var model = (inventory is null) switch
        {
            true => ProductInventoryReadModelMapper.Map(product),
            _ => ProductInventoryReadModelMapper.Map(product, inventory)
        };

        if (productUpdated)
        {
            return model;
        }
        
        var error = new Error(ErrorCodes.Product.Unchanged, "No changes found");
        return (model, error);
    }
    
    private async Task PublishEventsAsync(
        IReadOnlyCollection<DomainEvent> events,
        CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await publisher.PublishAsync(@event, cancellationToken);
        }
    }
}