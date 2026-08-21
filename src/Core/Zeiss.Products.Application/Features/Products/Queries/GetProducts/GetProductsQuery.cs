using MediatR;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProducts;

public sealed record GetProductsQuery : PagedQuery, IRequest<Result<GetProductsResult>>;