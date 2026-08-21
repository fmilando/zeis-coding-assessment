using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.GetByStockLevel;

internal sealed class GetByStockLevelQueryHandler(
    IProductInventoryReadRepository repository
) : IRequestHandler<GetByStockLevelQuery, Result<GetByStockLevelResult>>
{
    public async Task<Result<GetByStockLevelResult>> Handle(
        GetByStockLevelQuery request,
        CancellationToken cancellationToken)
    {
        var pagedResult = await repository.GetByStockLevelAsync(
            request.MinQuantity,
            request.MaxQuantity,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return new GetByStockLevelResult(
            pagedResult.Result,
            pagedResult.Metadata);
    }
}