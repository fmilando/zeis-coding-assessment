using FluentValidation;
using Zeiss.Products.Domain.Constants;

namespace Zeiss.Products.Application.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThanOrEqualTo(ProductConstants.IdStartValue)
            .LessThanOrEqualTo(ProductConstants.IdMaxValue)
            .WithName(nameof(DeleteProductCommand.ProductId));
    }
}