namespace ElectronicStore.Domain.Contracts.Repositories;

/// <summary>
/// Unit of Work паттерн для управления сохранением изменений
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохраняет все изменения в БД
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
