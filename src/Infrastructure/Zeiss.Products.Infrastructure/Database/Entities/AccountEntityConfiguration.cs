using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zeiss.Products.Infrastructure.Database.Entities;

internal sealed class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.ToTable(DbConstants.AccountSchemaName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn();

        builder.Property(x => x.ClientId)
               .HasMaxLength(32)
               .IsRequired();
        
        builder.Property(x => x.ClientSecret)
               .HasMaxLength(32)
               .IsRequired();
        
        builder.Property(x => x.IsLocked)
               .HasDefaultValue(false)
               .IsRequired();
        
        builder.Property(x => x.CreatedAt)
               .ValueGeneratedOnAdd()
               .IsRequired();
        
        builder.Property(x => x.UpdatedAt);
    }
}