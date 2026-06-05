using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db;

public class ElectronicStoreDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Brand>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Review>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Cart>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Purchase>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrderItem>()
            .HasOne<Product>()
            .WithMany()
            .IsRequired();

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Cart)
            .IsRequired(false);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Purchase)
            .IsRequired(false);
    }
}
