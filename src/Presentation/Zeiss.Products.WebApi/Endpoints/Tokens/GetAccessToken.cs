using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Zeiss.Products.WebApi.Contracts;
using Zeiss.Products.WebApi.Security;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Tokens;

internal static class GetAccessToken
{
    public static async Task<IResult> HandleAsync(
        ILogger logger,
        IConfiguration configuration,
        [FromBody] TokenRequest request)
    {
        if (AreCredentialsValid(request.SecretId, request.SecretKey) is false)
        {
            return Results.BadRequest(new
            {
                message = "Provided credentials are not valid"
            });
        }
        
        var claims = new List<Claim>()
        {
            new (JwtSettings.SecretIdClaimName, request.SecretId),
            new (JwtSettings.SecretKeyClaimName, request.SecretKey),
        };
        
        var (token, expiration) = IssueJwtToken(claims, configuration);
        logger.Information("Issued access token for {SecretId}", request.SecretId);
        
        return Results.Ok(new { token, expiration });
    }

    private static bool AreCredentialsValid(string secretId, string secretKey)
    {
        //Check the credentials against an official authorized list of clients
        //This could be a database or an API call.
        return true;
    }
    
    
    private static (string Token, DateTime Expiration) IssueJwtToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var settings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;
        var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);
        var expiration = DateTime.UtcNow.AddMinutes(settings.TokenExpirationMinutes);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.UtcNow,
            Expires = expiration,
            Issuer = settings.Issuer,
            Audience = settings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return (handler.WriteToken(token), expiration);
    }
}