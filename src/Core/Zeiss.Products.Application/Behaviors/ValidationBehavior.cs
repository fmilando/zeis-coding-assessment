using FluentValidation;
using MediatR;

namespace Zeiss.Products.Application.Behaviors;

internal class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken
    )
    {
        if (validators.Any() is false)
        {
            return await next(cancellationToken);
        }

        var validationResults = await Task.WhenAll(
            validators.Select(async x => await x.ValidateAsync(request, cancellationToken)));

        var errors = validationResults
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        return await next(cancellationToken);
    }
}