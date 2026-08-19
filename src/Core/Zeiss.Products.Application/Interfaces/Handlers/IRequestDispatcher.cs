using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Interfaces.Handlers;

public interface IRequestDispatcher
{
    Task<Result<TResponse>> DispatchAsync<TRequest, TResponse>(
        TRequest request, 
        CancellationToken cancellationToken
    ) where TRequest: class where TResponse : class;
}