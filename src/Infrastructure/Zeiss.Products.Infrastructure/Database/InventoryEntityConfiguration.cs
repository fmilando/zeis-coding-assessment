using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeiss.Products.Infrastructure.Database.Entities;

namespace Zeiss.Products.Infrastructure.Database;

internal sealed class InventoryEntityConfiguration : IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.ToTable(DbConstants.InventorySchemaName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn();

        builder.Property(x => x.ProductId)
               .IsRequired();
    }
}