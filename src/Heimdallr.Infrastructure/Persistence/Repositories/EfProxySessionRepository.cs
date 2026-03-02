using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using Heimdallr.Domain.Interfaces.Persistence.Repositories;
using Heimdallr.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Persistence.Repositories;

public class EfProxySessionRepository(ApplicationDbContext context) : IProxySessionRepository
{
    private DbSet<ProxySession> ProxySessions => context.Set<ProxySession>();

    public async Task<IEnumerable<ProxySession>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await ProxySessions.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProxySession>> GetActiveSessionsAsync(CancellationToken cancellationToken = default)
    {
        return await ProxySessions
            .AsNoTracking()
            .Where(s => s.Status == SessionStatus.Active)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProxySession>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await ProxySessions
            .AsNoTracking()
            .Where(s => s.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProxySession>> GetByMeterIdAsync(Guid meterId, CancellationToken cancellationToken = default)
    {
        return await ProxySessions
            .AsNoTracking()
            .Where(s => s.MeterId == meterId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProxySession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await ProxySessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task AddAsync(ProxySession session, CancellationToken cancellationToken = default)
    {
        await ProxySessions.AddAsync(session, cancellationToken);
    }

    public void Update(ProxySession session)
    {
        ProxySessions.Update(session);
    }

    public void Delete(ProxySession session)
    {
        ProxySessions.Remove(session);
    }
}
