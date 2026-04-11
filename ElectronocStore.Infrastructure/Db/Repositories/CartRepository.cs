using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class CartRepository(ElectronicStoreDbContext dbContext) : ICartRepository
{
    public async Task Create(Cart entity)
    {
        await dbContext.Carts.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Carts
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyCollection<Cart>> GetAll()
    {
        return await dbContext.Carts
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cart?> GetById(Guid id)
    {
        return await dbContext.Carts
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);

    }

    public async Task Update(Cart entity)
    {
        await dbContext.Carts
            .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.UserId, entity.UserId)
                .SetProperty(c => c.Products, entity.Products)
             );
    }
}
