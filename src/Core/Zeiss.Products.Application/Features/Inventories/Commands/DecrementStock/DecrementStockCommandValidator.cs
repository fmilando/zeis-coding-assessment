using FluentValidation;
using Zeiss.Products.Domain.Constants;

namespace Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;

internal sealed class DecrementStockCommandValidator : AbstractValidator<DecrementStockCommand>
{
    public DecrementStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThanOrEqualTo(ProductConstants.IdStartValue)
            .LessThanOrEqualTo(ProductConstants.IdMaxValue)
            .WithName(nameof(DecrementStockCommand.ProductId));

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithName(nameof(DecrementStockCommand.Quantity));
    }
}