using MediatR;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Accounts.Queries.ValidateCredentials;

public sealed record ValidateCredentialsQuery(
    string ClientId, 
    string ClientSecret) : IRequest<Result<bool>>;