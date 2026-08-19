using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.GetByStockLevel;

internal sealed class GetByStockLevelQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<GetByStockLevelQuery, GetByStockLevelResult>
{
    public async Task<Result<GetByStockLevelResult>> HandleAsync(
        GetByStockLevelQuery request,
        CancellationToken cancellationToken)
    {
        var pagedResult = await repository.GetByStockLevelAsync(
            request.MinQuantity,
            request.MaxQuantity,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        var result = new GetByStockLevelResult(
            pagedResult.Result,
            pagedResult.PaginationInfo);

        return new Result<GetByStockLevelResult>(result);
    }
}