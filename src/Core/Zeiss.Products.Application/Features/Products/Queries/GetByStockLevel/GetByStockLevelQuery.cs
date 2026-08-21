using MediatR;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries.GetByStockLevel;

public sealed record GetByStockLevelQuery(
    int? MinQuantity, 
    int? MaxQuantity
) : PagedQuery, IRequest<Result<GetByStockLevelResult>>;