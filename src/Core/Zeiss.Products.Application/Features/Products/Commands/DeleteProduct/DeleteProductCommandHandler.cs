using System.Data;
using MediatR;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository products,
    IEventPublisher publisher
) : IRequestHandler<DeleteProductCommand, Result<Void>>
{
    public async Task<Result<Void>> Handle(
        DeleteProductCommand request, 
        CancellationToken cancellationToken)
    {
        await unitOfWork.StartAsync(IsolationLevel.RepeatableRead, cancellationToken);
        var product = await products.GetAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return new Error(
                ErrorCodes.Product.NotFound,
                $"Product {request.ProductId} not found"
            );
        }

        product.Delete();
        await products.DeleteAsync(product, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var @event = product.Events.First();
        await publisher.PublishAsync(@event, cancellationToken);
        product.ClearEvents();

        return new Void();
    }
}