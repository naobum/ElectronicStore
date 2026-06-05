namespace ElectronicStore.Domain.Contracts.Repositories;

public interface ICommonRepository<TEntity>
{
    Task Create(TEntity entity);

    Task<TEntity?> GetById(long id);

    Task<IReadOnlyCollection<TEntity>> GetAll();

    Task Update(TEntity entity);

    Task Delete(long id);
}
