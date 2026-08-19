using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Interfaces.Handlers;

internal sealed class RequestDispatcher(IServiceProvider provider) : IRequestDispatcher
{
    public async Task<Result<TResponse>> DispatchAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken
    ) where TRequest : class where TResponse : class
    {
        var handler = provider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var validator = provider.GetService<IValidator<TRequest>>();

        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid is false)
            {
                var errors = validationResult.Errors.Select(x => new Error(x.ErrorCode, x.ErrorMessage)).ToArray();
                return errors;
            }
        }

        return await handler.HandleAsync(request, cancellationToken);
    }
}