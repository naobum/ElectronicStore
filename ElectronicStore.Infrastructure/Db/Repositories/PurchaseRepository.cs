using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class PurchaseRepository(ElectronicStoreDbContext dbContext) : IPurchaseRepository
{
    public async Task Create(Purchase entity)
    {
        await dbContext.Purchases.AddAsync(entity);
    }

    public async Task Delete(long id)
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

    public async Task<Purchase?> GetById(long id)
    {
        return await dbContext.Purchases
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyCollection<Purchase>> GetByUserId(long userId)
    {
        return await dbContext.Purchases
            .AsNoTracking()
            .Where(p => p.User.Id == userId)
            .ToListAsync();
    }

    public async Task Update(Purchase entity)
    {
        await dbContext.Purchases
            .Where(p => p.Id == entity.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.User, entity.User)
                .SetProperty(p => p.DateTime, entity.DateTime)
                .SetProperty(p => p.Items, entity.Items)
            );
    }
}
