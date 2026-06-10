using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicStore.Infrastructure.Db.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .Property(oi => oi.Id)
            .ValueGeneratedOnAdd();

        builder
            .HasOne(oi => oi.Product)
            .WithMany()
            .IsRequired();

        builder
            .HasOne(oi => oi.Cart)
            .WithMany(cart => cart.Items);

        builder
            .HasOne(oi => oi.Purchase)
            .WithMany(p => p.Items);
    }
}
