using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Interfaces.Persistence.Repositories;
using Heimdallr.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Persistence.Repositories;

public class EfMeterRepository(ApplicationDbContext context) : IMeterRepository
{
    private DbSet<Meter> Meters => context.Meters;

    public async Task<IEnumerable<Meter>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Meters
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Meter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Meters
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Meters
            .AsNoTracking()
            .AnyAsync(m => m.Id == id, cancellationToken);
    }

    public async Task AddAsync(Meter meter, CancellationToken cancellationToken = default)
    {
        await Meters.AddAsync(meter, cancellationToken);
    }

    public void Update(Meter meter)
    {
        Meters.Update(meter);
    }

    public void Delete(Meter meter)
    {
        Meters.Remove(meter);
    }
}
