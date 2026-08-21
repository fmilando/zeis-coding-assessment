using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.SearchProducts;

internal sealed class SearchProductsQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<SearchProductsQuery, Result<SearchProductsResult>>
{
    public async Task<Result<SearchProductsResult>> Handle(
        SearchProductsQuery query,
        CancellationToken cancellationToken)
    {
        var pagedResult = await repository.SearchByNameAsync(
            query.Name,
            query.PageNumber,
            query.PageSize,
            cancellationToken);

        return new SearchProductsResult(
            pagedResult.Result,
            pagedResult.Metadata);
    }
}