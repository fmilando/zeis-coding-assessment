using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Infrastructure.Repositories;

namespace Zeiss.Products.Infrastructure.Database;

internal static class DatabaseExtensions
{
    public static void AddDatabase(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services.AddDbContext<PersistenceDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(DbConstants.ConnectionStringName)!;
            options.UseNpgsql(connectionString);
        });
        
        services.AddDbContextFactory<PersistenceDbContext>(lifetime: ServiceLifetime.Scoped)
                .AddScoped<IDbConnectionFactory, NpgsqlConnectionFactory>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IInventoryRepository, InventoryRepository>()
                .AddScoped<IProductInventoryReadRepository, ProductInventoryReadRepository>()
                .AddScoped<DbErrorInterceptor>();
    }
}