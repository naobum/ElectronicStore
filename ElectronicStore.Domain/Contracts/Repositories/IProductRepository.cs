using ElectronicStore.Domain.Models;

namespace ElectronicStore.Domain.Contracts.Repositories;

public interface IProductRepository
{
    Task Create(Product product, CancellationToken cancellationToken);

    Task<Product?> GetProductById(long id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Product>> GetProductsWithFilter(ProductFilter productFilter, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Product>> GetAll();

    Task<Product?> GetById(long id);

    Task Update(Product entity);

    Task Delete(long id);
}
