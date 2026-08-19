using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductCommandHandler(
    IProductRepository products,
    IEventPublisher publisher
) : BaseCommandHandler<DeleteProductCommand, DeleteProductResult>(products)
{
    private readonly IProductRepository _products = products;

    protected override async Task<Result<DeleteProductResult>> HandleAsync(
        DeleteProductCommand request,
        Product product,
        CancellationToken cancellationToken)
    {
        product.Delete();
        await _products.DeleteAsync(product, cancellationToken);

        var @event = product.Events.First();
        await publisher.PublishAsync(@event, cancellationToken);
        product.ClearEvents();

        return new Result<DeleteProductResult>(new DeleteProductResult());
    }
}