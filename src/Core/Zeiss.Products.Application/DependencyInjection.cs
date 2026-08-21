using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Zeiss.Products.Application.Behaviors;

namespace Zeiss.Products.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            //Register requests and request handlers
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            //Register behavior to trigger request validators
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}
