using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Features;

internal abstract class BaseCommandHandler<TCommand, TResult>(IProductRepository products)
    : IRequestHandler<TCommand, TResult> where TCommand : BaseCommand
{
    public async Task<Result<TResult>> HandleAsync(TCommand request, CancellationToken cancellationToken)
    {
        var product = await products.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is not null)
        {
            return await HandleAsync(request, product, cancellationToken);
        }

        var error = new Error(
            ErrorCodes.ProductNotFound,
            $"Product {request.ProductId} not found");

        return new Result<TResult>([error]);
    }

    protected abstract Task<Result<TResult>> HandleAsync(
        TCommand request,
        Product product,
        CancellationToken cancellationToken);
}