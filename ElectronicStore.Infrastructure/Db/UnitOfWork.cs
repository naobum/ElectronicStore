using ElectronicStore.Domain.Contracts.Repositories;

namespace ElectronocStore.Infrastructure.Db;

/// <summary>
/// Unit of Work паттерн для управления сохранением изменений
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ElectronicStoreDbContext _dbContext;

    public UnitOfWork(ElectronicStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Сохраняет все изменения в БД
    /// </summary>
    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
