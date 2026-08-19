using FluentValidation;
using Zeiss.Products.Domain.Constants;

namespace Zeiss.Products.Application.Features.Products.Queries.SearchProducts;

internal sealed class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
{
    public SearchProductsQueryValidator()
    {
        Include(new PaginatedQueryValidator());
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(ProductConstants.NameMaxLength)
            .WithName(nameof(SearchProductsQuery.Name));
    }
}