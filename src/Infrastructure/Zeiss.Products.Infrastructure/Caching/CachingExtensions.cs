using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Zeiss.Products.Application.Interfaces;

namespace Zeiss.Products.Infrastructure.Caching;

internal static class CachingExtensions
{
    public static void AddCaching(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddOptions<RedisSettings>()
            .Bind(configuration.GetSection(RedisSettings.SectionName))
            .ValidateOnStart();

        services.AddScoped<IDistributedCacheLock, DistributedCacheLock>();
        services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<RedisSettings>>().Value;
            var options = ConfigurationOptions.Parse(settings.ConnectionString);
            options.User = null;
            return ConnectionMultiplexer.Connect(options);
        });
    }

}