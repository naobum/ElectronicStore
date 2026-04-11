using ElectronicStore.Domain.Models;

namespace ElectronicStore.Domain.Contracts.Repositories;

public interface IPurchaseRepository : ICommonRepository<Purchase>
{
    Task<IReadOnlyCollection<Purchase>> GetByUserId(Guid userId);
}
