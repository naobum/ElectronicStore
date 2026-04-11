using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class ProductRepository(ElectronicStoreDbContext dbContext) : IProductRepository
{
    public async Task Create(Product entity)
    {
        await dbContext.Products.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyCollection<Product>> GetAll()
    {
        return await dbContext.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Product entity)
    {
        await dbContext.Products
            .Where(p => p.Id == entity.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.Name, entity.Name)
                .SetProperty(p => p.BrandId, entity.BrandId)
                .SetProperty(p => p.Description, entity.Description)
                .SetProperty(p => p.Rating, entity.Rating)
                .SetProperty(p => p.Reviews, entity.Reviews)
            );
    }
}
