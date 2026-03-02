using Heimdallr.Domain.Entities;

namespace Heimdallr.Domain.Interfaces.Persistence.Repositories;

public interface IMeterRepository
{
    Task<IEnumerable<Meter>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Meter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Meter meter, CancellationToken cancellationToken = default);

    void Update(Meter meter);

    void Delete(Meter meter);
}
