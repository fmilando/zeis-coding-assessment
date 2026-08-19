namespace Zeiss.Products.Application.Features.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
    long ProductId
) : BaseCommand(ProductId);