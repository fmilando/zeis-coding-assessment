using MediatR;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries.SearchProducts;

public sealed record SearchProductsQuery(
    string Name) : PagedQuery, IRequest<Result<SearchProductsResult>>;