using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Zeiss.Products.Infrastructure.Database.Entities;

namespace Zeiss.Products.Infrastructure.Database;

internal sealed class PersistenceDbContext(
    DbContextOptions<PersistenceDbContext> options,
    DbErrorInterceptor interceptor
) : DbContext(options)
{
    //Application entities
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<InventoryEntity> Inventory { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }

    //Messaging entities
    public DbSet<InboxState> InboxStates { get; set; }
    public DbSet<OutboxState> OutboxStates { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        builder.AddInboxStateEntity();
        builder.AddOutboxStateEntity();
        builder.AddOutboxMessageEntity();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(interceptor);
        base.OnConfiguring(optionsBuilder);
    }
}