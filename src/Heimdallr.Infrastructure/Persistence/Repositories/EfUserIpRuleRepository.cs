using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Interfaces.Persistence.Repositories;
using Heimdallr.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Persistence.Repositories;

public class EfUserIpRuleRepository(ApplicationDbContext context) : IUserIpRuleRepository
{
    private DbSet<UserIpRule> UserIpRules => context.UserIpRules;
    
    public async Task<IEnumerable<UserIpRule>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await UserIpRules.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserIpRule>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await UserIpRules
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserIpRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await UserIpRules
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await UserIpRules.AsNoTracking().AnyAsync(r => r.Id == id, cancellationToken);
    }

    public async Task AddAsync(UserIpRule rule, CancellationToken cancellationToken = default)
    {
        await UserIpRules.AddAsync(rule, cancellationToken);
    }

    public void Update(UserIpRule rule)
    {
        UserIpRules.Update(rule);
    }

    public void Delete(UserIpRule rule)
    {
        UserIpRules.Remove(rule);
    }
}
