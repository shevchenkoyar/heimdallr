using Heimdallr.Domain.Entities;

namespace Heimdallr.Domain.Interfaces.Persistence.Repositories;

public interface IProxyPortRepository
{
    Task<IEnumerable<ProxyPort>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<ProxyPort>> GetFreePortsAsync(CancellationToken cancellationToken = default);

    Task<ProxyPort?> GetByPortAsync(int port, CancellationToken cancellationToken = default);
    
    Task AddAsync(ProxyPort proxyPort, CancellationToken cancellationToken = default);

    void Update(ProxyPort proxyPort);

    void Delete(ProxyPort proxyPort);
}
