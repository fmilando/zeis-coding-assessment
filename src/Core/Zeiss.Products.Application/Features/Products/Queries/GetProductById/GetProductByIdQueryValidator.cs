using FluentValidation;
using Zeiss.Products.Domain.Constants;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThanOrEqualTo(ProductConstants.IdStartValue)
            .LessThanOrEqualTo(ProductConstants.IdMaxValue)
            .WithName(nameof(GetProductByIdQuery.ProductId));
    }
}