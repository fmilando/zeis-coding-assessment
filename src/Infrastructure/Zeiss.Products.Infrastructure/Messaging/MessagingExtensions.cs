using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Infrastructure.Database;

namespace Zeiss.Products.Infrastructure.Messaging;

internal static class MessagingExtensions
{
    public static void AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();

        services.AddMassTransit(bus =>
        {
            bus.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));
            bus.AddEntityFrameworkOutbox<PersistenceDbContext>(options =>
            {
                options.UseBusOutbox();
                options.UsePostgres();
                options.QueryDelay = TimeSpan.FromSeconds(1);
            });

            bus.UsingRabbitMq((context, rabbit) =>
            {
                var settings = configuration.GetRequiredSection(RabbitMqSettings.SectionName).Get<RabbitMqSettings>()!;
                rabbit.Host(settings.Host, settings.VirtualHost ?? "/", options =>
                {
                    options.Username(settings.Username);
                    options.Password(settings.Password);
                });

                rabbit.ConfigureEndpoints(context);
            });
        });
    }

}