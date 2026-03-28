using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.ProxySessions.Dtos;
using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Contracts.ProxySessions.Queries.GetActiveProxySessions;

[UsedImplicitly]
public sealed class GetActiveProxySessionsQueryHandler(
    IApplicationDbContext dbContext) : IQueryHandler<GetActiveProxySessionsQuery, IReadOnlyList<ProxySessionDto>>
{
    public async Task<Result<IReadOnlyList<ProxySessionDto>>> Handle(GetActiveProxySessionsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<ProxySession> source = dbContext.ProxySessions
            .AsNoTracking()
            .Where(x => x.Status == SessionStatus.Starting || x.Status == SessionStatus.Active);

        if (query.UserId.HasValue)
        {
            source = source.Where(x => x.UserId == query.UserId.Value);
        }

        return await source
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
