using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class PurchaseRepository(ElectronicStoreDbContext dbContext) : IPurchaseRepository
{
    public async Task Create(Purchase entity)
    {
        await dbContext.Purchases.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Purchases
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyCollection<Purchase>> GetAll()
    {
        return await dbContext.Purchases
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Purchase?> GetById(Guid id)
    {
        return await dbContext.Purchases
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyCollection<Purchase>> GetByUserId(Guid userId)
    {
        return await dbContext.Purchases
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public Task Update(Purchase entity)
    {
        throw new NotImplementedException();
    }
}
