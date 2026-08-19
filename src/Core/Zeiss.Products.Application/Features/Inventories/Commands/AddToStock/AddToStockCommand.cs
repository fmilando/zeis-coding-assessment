namespace Zeiss.Products.Application.Features.Inventories.Commands.AddToStock;

public sealed record AddToStockCommand(
    long ProductId,
    int Quantity
) : BaseCommand(ProductId);