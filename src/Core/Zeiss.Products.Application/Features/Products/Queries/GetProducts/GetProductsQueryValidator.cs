using FluentValidation;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        Include(new PaginatedQueryValidator());
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithName(nameof(GetProductsQuery.PageNumber));

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithName(nameof(GetProductsQuery.PageSize));
    }
}