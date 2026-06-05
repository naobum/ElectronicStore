using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class UserRepository(ElectronicStoreDbContext dbContext) : IUserRepository
{
    public async Task CreateUser(User user, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(user);
    }

    public async Task DeleteUser(long id, CancellationToken cancellationToken)
    {
        await dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<User?> GetUserById(long id, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<User>> GetUsers(CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }
}
