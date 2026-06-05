using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class ProductRepository(ElectronicStoreDbContext dbContext) : IProductRepository
{
    public async Task<Product?> GetProductById(long id, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Product>> GetProductsWithFilter(ProductFilter productFilter, CancellationToken cancellationToken)
    {
        var query = dbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(productFilter.Name))
        {
            query = query.Where(p => p.Name.Contains(productFilter.Name));
        }

        if (productFilter.BrandId.HasValue)
        {
            query = query.Where(p => p.BrandId == productFilter.BrandId);
        }

        if (productFilter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= productFilter.MinPrice);
        }

        if (productFilter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= productFilter.MaxPrice);
        }

        if (productFilter.MinRating.HasValue)
        {
            query = query.Where(p => p.Rating >= productFilter.MinRating);
        }

        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task Create(Product entity)
    {
        await dbContext.Products.AddAsync(entity);
    }

    public async Task Delete(long id)
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

    public async Task<Product?> GetById(long id)
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
            );
    }
}
