using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.SearchProducts;

internal sealed class SearchProductsQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<SearchProductsQuery, SearchProductsResult>
{
    public async Task<Result<SearchProductsResult>> HandleAsync(
        SearchProductsQuery query,
        CancellationToken cancellationToken)
    {
        var pagedResult = await repository.SearchByNameAsync(
            query.Name,
            query.PageNumber,
            query.PageSize,
            cancellationToken);

        var result = new SearchProductsResult(
            pagedResult.Result,
            pagedResult.PaginationInfo);

        return new Result<SearchProductsResult>(result);
    }
}