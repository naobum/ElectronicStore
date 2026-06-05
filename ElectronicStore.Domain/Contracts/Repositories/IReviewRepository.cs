using ElectronicStore.Domain.Models;

namespace ElectronicStore.Domain.Contracts.Repositories;

public interface IReviewRepository
{
    public Task Create(Review entity, CancellationToken cancellationToken);

    public Task<Review?> GetReviewById(long id, CancellationToken cancellationToken);

    public Task<Review?> GetReviewByUserIdAndProductId(long userId, long productId, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Review>> GetReviewsByUserId(long userId, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Review>> GetReviewsByProductId(long productId, CancellationToken cancellationToken);
}