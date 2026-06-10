using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicStore.Infrastructure.Db.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder
            .HasMany(p => p.Items)
            .WithOne(oi => oi.Purchase);
    }
}
