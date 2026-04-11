using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class BrandRepository(ElectronicStoreDbContext dbContext) : IBrandRepository
{
    public async Task Create(Brand entity)
    {
        await dbContext.Brands.AddAsync(entity);
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Brands
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyCollection<Brand>> GetAll()
    {
        return await dbContext.Brands
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Brand?> GetById(Guid id)
    {
        return await dbContext.Brands
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task Update(Brand entity)
    {
        await dbContext.Brands
            .Where(b => b.Id == entity.Id)
            .ExecuteUpdateAsync(b => b
                .SetProperty(b => b.Name, entity.Name)
                .SetProperty(b => b.Description, entity.Description)
            );
    }
}
