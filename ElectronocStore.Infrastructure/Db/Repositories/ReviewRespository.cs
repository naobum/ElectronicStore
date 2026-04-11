using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class ReviewRespository(ElectronicStoreDbContext dbContext) : IReviewRepository
{
    public async Task Create(Review entity)
    {
        await dbContext.Reviews.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Reviews
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyCollection<Review>> GetAll()
    {
        return await dbContext.Reviews
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Review?> GetById(Guid id)
    {
        return await dbContext.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task Update(Review entity)
    {
        await dbContext.Reviews
            .Where(r => r.Id == entity.Id)
            .ExecuteUpdateAsync(r => r
                .SetProperty(r => r.UserId, entity.UserId)
                .SetProperty(r => r.ProductId, entity.ProductId)
                .SetProperty(r => r.Rate, entity.Rate)
                .SetProperty(r => r.Comment, entity.Comment)
            );
    }
}
