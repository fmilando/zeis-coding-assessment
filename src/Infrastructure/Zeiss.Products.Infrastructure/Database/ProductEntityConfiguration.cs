using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeiss.Products.Domain.Constants;
using Zeiss.Products.Infrastructure.Database.Entities;

namespace Zeiss.Products.Infrastructure.Database;

internal sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable(DbConstants.ProductSchemaName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(
                    startValue: ProductConstants.IdStartValue,
                    maxValue: ProductConstants.IdMaxValue,
                    incrementBy: 1
                );

        builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ProductConstants.NameMaxLength);

        builder.Property(x => x.Sku)
                .IsRequired()
                .HasMaxLength(ProductConstants.SkuMaxLength);

        builder.Property(x => x.Description)
            .HasMaxLength(ProductConstants.DescriptionMaxLength);

        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}