using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronocStore.Infrastructure.Db.Repositories;

public class UserRepository(ElectronicStoreDbContext dbContext) : IUserRepository
{
    public async Task Create(User entity)
    {
        await dbContext.Users.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyCollection<User>> GetAll()
    {
        return await dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Update(User entity)
    {
        await dbContext.Users
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Name, entity.Name)
            );
    }
}
