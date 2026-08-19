using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StackExchange.Redis;
using Zeiss.Products.Infrastructure.Caching;
using Zeiss.Products.Infrastructure.Database;
using Zeiss.Products.Infrastructure.Logging;
using Zeiss.Products.Infrastructure.Messaging;

namespace Zeiss.Products.Infrastructure.HealthChecks;

internal static class HealthCheckExtensions
{
    public static void AddHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var tags = new[]{ "ready" };
        var healthChecks= services.AddHealthChecks();
        
        healthChecks.AddNpgSql(_ => 
                configuration.GetConnectionString(DbConstants.ConnectionStringName)!, 
            name: "postgres-db", tags: tags);
        
        healthChecks.AddRabbitMQ(provider =>
        {
            var settings = configuration.GetRequiredSection(RabbitMqSettings.SectionName).Get<RabbitMqSettings>()!;
            return new ConnectionFactory
            {
                HostName = settings.Host,
                UserName = settings.Username,
                Password = settings.Password,
                VirtualHost = settings.VirtualHost,
                Port = settings.Port
            }.CreateConnectionAsync();
        }, name: "rabbitmq", tags: tags);
            
        healthChecks.AddRedis(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<RedisSettings>>().Value;
            return ConnectionMultiplexer.Connect(settings.ConnectionString);
        }, name: "redis", tags: tags);

        healthChecks.AddElasticsearch(options =>
        {
            var settings = configuration.GetRequiredSection(ElasticsearchSettings.SectionName).Get<ElasticsearchSettings>()!;
            options.UseServer(settings.Uri);
        }, name: "elasticsearch", tags: tags);
    }
}