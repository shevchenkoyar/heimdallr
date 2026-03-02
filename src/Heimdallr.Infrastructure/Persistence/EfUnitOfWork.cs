using Heimdallr.Domain.Interfaces;
using Heimdallr.Domain.Interfaces.Persistence;
using Heimdallr.Infrastructure.Database;

namespace Heimdallr.Infrastructure.Persistence;

internal sealed class EfUnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
