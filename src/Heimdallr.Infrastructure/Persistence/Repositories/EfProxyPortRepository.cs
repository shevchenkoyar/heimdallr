using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using Heimdallr.Domain.Interfaces.Persistence.Repositories;
using Heimdallr.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Persistence.Repositories;

public class EfProxyPortRepository(ApplicationDbContext context) : IProxyPortRepository
{
    private DbSet<ProxyPort> ProxyPorts => context.ProxyPorts;

    public async Task<IEnumerable<ProxyPort>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await ProxyPorts.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProxyPort>> GetFreePortsAsync(CancellationToken cancellationToken = default)
    {
        return await ProxyPorts
            .AsNoTracking()
            .Where(p => p.State == ProxyPortState.Free)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProxyPort?> GetByPortAsync(int port, CancellationToken cancellationToken = default)
    {
        return await ProxyPorts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Port == port, cancellationToken);
    }

    public async Task AddAsync(ProxyPort proxyPort, CancellationToken cancellationToken = default)
    {
        await ProxyPorts.AddAsync(proxyPort, cancellationToken);
    }

    public void Update(ProxyPort proxyPort)
    {
        ProxyPorts.Update(proxyPort);
    }

    public void Delete(ProxyPort proxyPort)
    {
        ProxyPorts.Remove(proxyPort);
    }
}
