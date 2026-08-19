using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Zeiss.Products.Infrastructure.Logging;

public static class LoggingExtensions
{
    public static void AddApiLogging(
        this ConfigureHostBuilder builder,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.UseSerilog((context, provider, conf) =>
        {
            var settings = configuration.GetRequiredSection(ElasticsearchSettings.SectionName).Get<ElasticsearchSettings>()!;

            conf.ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(provider)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithCorrelationId()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(settings.Uri))
                {
                    IndexFormat = settings.IndexFormat,
                    AutoRegisterTemplate = true,
                    NumberOfReplicas = 1
                });
        });
    }
}