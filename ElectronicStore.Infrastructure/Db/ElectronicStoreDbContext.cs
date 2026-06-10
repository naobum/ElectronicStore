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

    public ElectronicStoreDbContext(DbContextOptions<ElectronicStoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ElectronicStoreDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
