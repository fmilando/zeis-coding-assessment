using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Accounts.Queries.ValidateCredentials;

public interface IAccountReadRepository
{
    Task<Result<bool>> IsValidAsync(
        string clientId,
        string clientSecret,
        CancellationToken cancellationToken);
}