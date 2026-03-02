using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Interfaces.Persistence.Repositories;
using Heimdallr.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Persistence.Repositories;

public class EfUserRepository(ApplicationDbContext context) : IUserRepository
{
    private DbSet<User> Users => context.Users;

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Users.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Users.AsNoTracking().AnyAsync(u => u.Id == id, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await Users.AddAsync(user, cancellationToken);
    }

    public void Update(User user)
    {
        Users.Update(user);
    }

    public void Delete(User user)
    {
        Users.Remove(user);
    }
}
