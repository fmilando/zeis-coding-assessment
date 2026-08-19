using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zeiss.Products.Infrastructure.Database;

namespace Zeiss.Products.Infrastructure.Builders;

/// <summary>
/// This design time host builder is used by EF Core Migrations to create and update the database
/// </summary>
public static class DesignTimeHostBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString(DbConstants.ConnectionStringName)!;
            var assemblyFullName = typeof(PersistenceDbContext).Assembly.FullName;
            
            services.AddDbContext<PersistenceDbContext>(builder =>
            {
                builder.UseNpgsql(
                    connectionString,
                    options => options.MigrationsAssembly(assemblyFullName)
                );
            });
        });
    }
}