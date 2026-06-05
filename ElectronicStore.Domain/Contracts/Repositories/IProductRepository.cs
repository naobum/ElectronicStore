using ElectronicStore.Domain.Models;

namespace ElectronicStore.Domain.Contracts.Repositories;

public interface IProductRepository
{
    public Task<Product?> GetProductById(long id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Product>> GetProductsWithFilter(ProductFilter productFilter, CancellationToken cancellationToken);
}
