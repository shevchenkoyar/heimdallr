using Heimdallr.Domain.Entities;

namespace Heimdallr.Domain.Interfaces.Persistence.Repositories;

public interface IMeterEndpointRepository
{
    Task<IReadOnlyList<MeterEndpoint>> GetByMeterIdAsync(Guid meterId, CancellationToken cancellationToken = default);
    Task AddAsync(MeterEndpoint endpoint, CancellationToken cancellationToken = default);
    void Update(MeterEndpoint endpoint);
    void Remove(MeterEndpoint endpoint);
}
