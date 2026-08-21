using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Zeiss.Products.WebApi.Security;

internal static class SecurityExtensions
{
    public static void AddApiSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var settings = configuration.GetRequiredSection(JwtSettings.SectionName).Get<JwtSettings>()!;
            var signingKey = Encoding.UTF8.GetBytes(settings.SecretKey);
            var decryptionKey = new SymmetricSecurityKey(signingKey[..32]);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = settings.Issuer,
                ValidateAudience = false,
                ValidAudience = settings.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = decryptionKey,
                TokenDecryptionKey = decryptionKey,
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,
                RequireExpirationTime = true
            };
        });

        services.AddAuthorization();
    }
}