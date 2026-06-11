using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class CartRepository(ElectronicStoreDbContext dbContext) : ICartRepository
{
    public async Task CreateCart(Cart cart, CancellationToken cancellationToken)
    {
        await dbContext.Carts.AddAsync(cart, cancellationToken);
    }

    public async Task<Cart?> GetCartByUserId(long userId, CancellationToken cancellationToken)
    {
        return await dbContext.Carts
            .Include(c => c.User)
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }
}
