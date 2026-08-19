using MassTransit;
using Microsoft.Extensions.Logging;
using Zeiss.Products.Application.Interfaces.Messaging;

namespace Zeiss.Products.Infrastructure.Messaging;

internal sealed class RabbitMqEventPublisher(
    IPublishEndpoint endpoint,
    ILogger<RabbitMqEventPublisher> logger
) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(
        TEvent @event, 
        CancellationToken cancellationToken
    ) where TEvent : class
    {
        var eventName = @event.GetType().Name;
        try
        { 
            await endpoint.Publish(@event, cancellationToken);
            logger.LogInformation("Published event {Event} with {EventData}", eventName, @event);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to publish event {EventName} with {EventData}: {Reason}", 
                eventName, 
                @event,
                ex.Message);
        }
    }
}