using ElectronicStore.Domain.Models;

namespace ElectronicStore.Domain.Contracts.Repositories;

public interface ICartRepository
{
    public Task<Cart?> GetCartByUserId(long userId, CancellationToken cancellationToken);

    public Task CreateCart(Cart cart, CancellationToken cancellationToken);
}
