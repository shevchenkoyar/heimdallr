using Heimdallr.Domain.Entities;

namespace Heimdallr.Domain.Interfaces.Persistence.Repositories;

public interface IProxySessionRepository
{
    Task<IEnumerable<ProxySession>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<ProxySession>> GetActiveSessionsAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<ProxySession>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProxySession>> GetByMeterIdAsync(Guid meterId, CancellationToken cancellationToken = default);
    
    Task<ProxySession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddAsync(ProxySession session, CancellationToken cancellationToken = default);
    
    void Update(ProxySession session);
    
    void Delete(ProxySession session);
}
