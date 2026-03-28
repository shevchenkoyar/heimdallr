using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Runtime;
using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Proxying;

public sealed class ProxyPortAllocator(
    IApplicationDbContext dbContext)
    : IProxyPortAllocator
{
    public async Task<int> ReserveFreePortAsync(CancellationToken cancellationToken = default)
    {
        ProxyPort port = await dbContext.ProxyPorts
                             .OrderBy(x => x.Port)
                             .FirstOrDefaultAsync(x => x.State == ProxyPortState.Free, cancellationToken)
                         ?? throw new InvalidOperationException("No free proxy ports are available.");

        port.State = ProxyPortState.Reserved;
        port.ReservedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return port.Port;
    }

    public async Task MarkPortInUseAsync(int port, CancellationToken cancellationToken = default)
    {
        ProxyPort entity = await dbContext.ProxyPorts
            .SingleAsync(x => x.Port == port, cancellationToken);

        entity.State = ProxyPortState.InUse;
        entity.LastUsedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ReleasePortAsync(int port, CancellationToken cancellationToken = default)
    {
        ProxyPort entity = await dbContext.ProxyPorts
            .SingleAsync(x => x.Port == port, cancellationToken);

        entity.State = ProxyPortState.Free;
        entity.ReservedAt = null;
        entity.LastUsedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
