using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Accounts.Queries.ValidateCredentials;

public sealed class ValidateCredentialsQueryHandler(
    IAccountReadRepository accounts
) : IRequestHandler<ValidateCredentialsQuery, Result<bool>>
{
    public Task<Result<bool>> Handle(
        ValidateCredentialsQuery request, 
        CancellationToken cancellationToken
    ) => accounts.IsValidAsync(request.ClientId, request.ClientSecret, cancellationToken);
}