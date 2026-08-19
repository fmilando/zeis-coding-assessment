using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeiss.Products.Infrastructure.Caching;
using Zeiss.Products.Infrastructure.Database;
using Zeiss.Products.Infrastructure.Messaging;
using Zeiss.Products.Infrastructure.HealthChecks;

namespace Zeiss.Products.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDatabase(configuration);
        services.AddMessaging(configuration);
        services.AddCaching(configuration);
        services.AddHealthChecks(configuration);
    }
}