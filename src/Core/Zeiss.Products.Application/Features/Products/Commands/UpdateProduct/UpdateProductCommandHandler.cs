using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Domain.Events;

namespace Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    IProductRepository products,
    IInventoryRepository inventories,
    IEventPublisher publisher
) : BaseCommandHandler<UpdateProductCommand, UpdateProductResult>(products)
{
    private readonly IProductRepository _products = products;

    protected override async Task<Result<UpdateProductResult>> HandleAsync(
        UpdateProductCommand request, 
        Product product, 
        CancellationToken cancellationToken)
    {
        product.SetName(request.Name);
        product.SetDescription(request.Description);
        product.SetPrice(request.Price);
        product.SetSku(request.Sku);
        
        var inventory = await inventories.GetAsync(request.ProductId, cancellationToken);

        if (product.Events.Count > 0)
        {
            product = await _products.UpdateAsync(product, cancellationToken);
            await PublishEventsAsync(product.Events, cancellationToken);
        }
        
        var model = (inventory is null) switch
        {
            true => ProductInventoryReadModelMapper.Map(product),
            _ => ProductInventoryReadModelMapper.Map(product, inventory)
        }; 
        
        var result = new UpdateProductResult(model);
        return new Result<UpdateProductResult>(result);
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