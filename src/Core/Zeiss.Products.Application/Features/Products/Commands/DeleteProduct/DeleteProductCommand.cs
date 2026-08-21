using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(int ProductId) : IRequest<Result<Void>>;