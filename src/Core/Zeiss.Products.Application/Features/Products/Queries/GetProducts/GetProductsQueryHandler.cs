using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<GetProductsQuery, Result<GetProductsResult>>
{
    public async Task<Result<GetProductsResult>> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var pagedResult = await repository.GetAsync(
            query.PageNumber,
            query.PageSize,
            cancellationToken);

        return new GetProductsResult(
            pagedResult.Result,
            pagedResult.Metadata);
    }
}