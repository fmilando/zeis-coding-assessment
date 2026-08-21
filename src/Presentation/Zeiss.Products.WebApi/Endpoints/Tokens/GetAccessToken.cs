using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Features.Accounts.Queries.ValidateCredentials;
using Zeiss.Products.WebApi.Contracts;
using Zeiss.Products.WebApi.Mappers;
using Zeiss.Products.WebApi.Security;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Tokens;

internal static class GetAccessToken
{
    public static async Task<IResult> HandleAsync(
        ISender sender,
        ILogger logger,
        IConfiguration configuration,
        [FromBody] TokenRequest request,
        HttpContext context)
    {
        var query = new ValidateCredentialsQuery(request.ClientId, request.ClientSecret);
        var result = await sender.Send(query, context.RequestAborted);

        if (result.IsError)
        {
            var response = result.ToApiResponse();
            var isLocked = result.Errors.Any(x => x.Code == ErrorCodes.Account.Locked);
            var isNotFound = result.Errors.Any(x => x.Code == ErrorCodes.Account.NotFound);

            return (isLocked, isNotFound) switch
            {
                (true,_) => Results.Json(response, statusCode: 423),
                (_,true) => Results.Json(response, statusCode: StatusCodes.Status401Unauthorized),
                _ => Results.BadRequest(response)
            };
        }

        var claims = new List<Claim>()
        {
            new (JwtSettings.UserUniqueIdClaimName, GetUserUniqueId(request))
        };

        var (token, expiration) = IssueJwtToken(claims, configuration);
        logger.Information("Issued access token for {ClientId}", request.ClientId);

        return Results.Ok(new { token, expiration }.ToApiResponse());
    }

    private static string GetUserUniqueId(TokenRequest request)
    {
        var bytes = Encoding.UTF8.GetBytes($"{request.ClientId}:{request.ClientSecret}");
        return Convert.ToHexStringLower(SHA256.HashData(bytes));
    }
    
    private static (string Token, DateTime Expiration) IssueJwtToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var settings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;
        var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);
        var expiration = DateTime.UtcNow.AddMinutes(settings.TokenExpirationMinutes);
        var securityKey = new SymmetricSecurityKey(secretKey[..32]);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.UtcNow,
            Expires = expiration,
            Issuer = settings.Issuer,
            Audience = settings.Audience,
            SigningCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256),
            EncryptingCredentials = new EncryptingCredentials(
                securityKey,
                SecurityAlgorithms.Aes256KW,
                SecurityAlgorithms.Aes256CbcHmacSha512)
        };

        var handler = new JwtSecurityTokenHandler();
        var jweToken = handler.CreateEncodedJwt(tokenDescriptor);
        
        return (jweToken, expiration);
    }
}