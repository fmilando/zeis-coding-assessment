using Dapper;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Features.Accounts.Queries.ValidateCredentials;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Infrastructure.Database.Repositories;

internal sealed class AccountReadRepository(
    IDbConnectionFactory connectionFactory
) : IAccountReadRepository
{
	
	public async Task<Result<bool>> IsValidAsync(string clientId, string clientSecret, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		using var connection = connectionFactory.Create();
		connection.Open();

		const string query = $"""
		                      SELECT "IsLocked" FROM "Accounts"
		                      WHERE "ClientId" = @clientId AND "ClientSecret" = @clientSecret
		                      LIMIT 1;
		                      """;

		var isLocked = (await connection.ExecuteScalarAsync<bool?>(query, new
		{
			clientId,
			clientSecret,
		}));

		if (isLocked is null)
		{
			return new Error(ErrorCodes.Account.NotFound, "Invalid credentials");
		}
		
		if (isLocked is true)
		{
			return new Error(ErrorCodes.Account.Locked, "Account is locked");
		}
		
		return isLocked!.Value;
	}
}