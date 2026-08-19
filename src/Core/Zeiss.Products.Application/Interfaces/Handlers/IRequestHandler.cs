using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Interfaces.Handlers;

public interface IRequestHandler<in TRequest, TResult> where TRequest : class
{
    Task<Result<TResult>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}