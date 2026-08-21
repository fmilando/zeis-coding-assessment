using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zeiss.Products.Infrastructure.Database.Entities;

internal sealed class InventoryEntityConfiguration : IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.ToTable(DbConstants.InventorySchemaName);
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.ProductId);
        builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn();

        builder.Property(x => x.ProductId)
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt)
               .ValueGeneratedOnUpdate();
    }
}