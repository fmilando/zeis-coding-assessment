using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<GetProductByIdQuery, Result<ProductInventoryReadModel>>
{
    public async Task<Result<ProductInventoryReadModel>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var model = await repository.GetByIdAsync(request.ProductId, cancellationToken);

        if (model is not null)
        {
            return model;
        }

        return new Error(
            ErrorCodes.Product.NotFound,
            $"Product {request.ProductId} not found"
        );
    }
}