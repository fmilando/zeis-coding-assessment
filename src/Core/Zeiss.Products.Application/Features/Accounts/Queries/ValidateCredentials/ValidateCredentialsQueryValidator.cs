using FluentValidation;

namespace Zeiss.Products.Application.Features.Accounts.Queries.ValidateCredentials;

public sealed class ValidateCredentialsQueryValidator : AbstractValidator<ValidateCredentialsQuery>
{
    public ValidateCredentialsQueryValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithName(nameof(ValidateCredentialsQuery.ClientId));
        
        RuleFor(x => x.ClientSecret)
            .NotEmpty()
            .WithName(nameof(ValidateCredentialsQuery.ClientSecret));
    }
}