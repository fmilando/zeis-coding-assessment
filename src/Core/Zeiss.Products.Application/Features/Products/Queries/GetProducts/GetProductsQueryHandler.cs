using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<Result<GetProductsResult>> HandleAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var pagedResult = await repository.GetAsync(
            query.PageNumber,
            query.PageSize,
            cancellationToken);

        var result = new GetProductsResult(
            pagedResult.Result,
            pagedResult.PaginationInfo);

        return new Result<GetProductsResult>(result);
    }
}