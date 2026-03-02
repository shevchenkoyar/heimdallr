using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Interfaces.Persistence.Repositories;
using Heimdallr.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Persistence.Repositories;

public sealed class EfMeterEndpointRepository(ApplicationDbContext context) : IMeterEndpointRepository
{
    private DbSet<MeterEndpoint> MeterEndpoints => context.MeterEndpoints;

    public async Task<IReadOnlyList<MeterEndpoint>> GetByMeterIdAsync(Guid meterId, CancellationToken cancellationToken = default)
    {
        return await MeterEndpoints
            .AsNoTracking()
            .Where(x => x.MeterId == meterId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MeterEndpoint endpoint, CancellationToken cancellationToken = default)
    {
        await MeterEndpoints.AddAsync(endpoint, cancellationToken);
    }

    public void Update(MeterEndpoint endpoint)
    {
        MeterEndpoints.Update(endpoint);
    }

    public void Remove(MeterEndpoint endpoint)
    {
        MeterEndpoints.Remove(endpoint);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
