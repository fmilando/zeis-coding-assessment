using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(
    int ProductId
) : IRequest<Result<ProductInventoryReadModel>>;