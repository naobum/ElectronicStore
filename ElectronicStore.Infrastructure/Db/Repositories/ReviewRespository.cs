using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class ReviewRespository(ElectronicStoreDbContext dbContext) : IReviewRepository
{
    /// <summary>
    /// Создает новый отзыв в БД
    /// </summary>
    public async Task Create(Review entity, CancellationToken cancellationToken)
    {
        await dbContext.Reviews.AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Получает отзыв по идентификатору
    /// </summary>
    public async Task<Review?> GetReviewById(long id, CancellationToken cancellationToken)
    {
        return await dbContext.Reviews
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получает отзыв пользователя на конкретный товар
    /// </summary>
    public async Task<Review?> GetReviewByUserIdAndProductId(long userId, long productId, CancellationToken cancellationToken)
    {
        return await dbContext.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId, cancellationToken);
    }

    /// <summary>
    /// Получает все отзывы пользователя
    /// </summary>
    public async Task<IReadOnlyCollection<Review>> GetReviewsByUserId(long userId, CancellationToken cancellationToken)
    {
        return await dbContext.Reviews
            .Where(r => r.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Получает все отзывы на товар
    /// </summary>
    public async Task<IReadOnlyCollection<Review>> GetReviewsByProductId(long productId, CancellationToken cancellationToken)
    {
        return await dbContext.Reviews
            .Where(r => r.ProductId == productId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
