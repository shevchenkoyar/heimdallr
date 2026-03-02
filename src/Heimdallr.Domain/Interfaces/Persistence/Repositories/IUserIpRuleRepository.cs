using Heimdallr.Domain.Entities;

namespace Heimdallr.Domain.Interfaces.Persistence.Repositories;

public interface IUserIpRuleRepository
{
    Task<IEnumerable<UserIpRule>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<UserIpRule>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<UserIpRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(UserIpRule rule, CancellationToken cancellationToken = default);

    void Update(UserIpRule rule);

    void Delete(UserIpRule rule);
}
