using ElectronicStore.Domain.Models;

namespace ElectronicStore.Domain.Contracts.Repositories;

public interface IUserRepository
{
    public Task CreateUser(User user, CancellationToken cancellationToken);

    public Task<User?> GetUserById(long id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<User>> GetUsers(CancellationToken cancellationToken);

    public Task DeleteUser(long id, CancellationToken cancellationToken);
}
