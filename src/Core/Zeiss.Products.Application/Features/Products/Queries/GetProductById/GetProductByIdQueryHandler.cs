using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<Result<GetProductByIdResult>> HandleAsync(
        GetProductByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var model = await repository.GetByIdAsync(request.ProductId, cancellationToken);

        if (model is not null)
        {
            return new GetProductByIdResult(model);
        }
        
        var error = new Error(
            ErrorCodes.ProductNotFound,
            $"Product {request.ProductId} not found");
        
        return new Result<GetProductByIdResult>([error]);
    }
}