using FluentValidation;
using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries;

internal sealed class PaginatedQueryValidator : AbstractValidator<PagedQuery>
{
    public PaginatedQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithName(nameof(PagedQuery.PageNumber));

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithName(nameof(PagedQuery.PageSize));
    }
}