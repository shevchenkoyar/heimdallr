using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.ProxySessions.Dtos;
using Heimdallr.Domain.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Contracts.ProxySessions.Queries.GetProxySessionById;

[UsedImplicitly]
public sealed class GetProxySessionByIdQueryHandler(
    IApplicationDbContext dbContext) : IQueryHandler<GetProxySessionByIdQuery, ProxySessionDto>
{
    public async Task<Result<ProxySessionDto>> Handle(GetProxySessionByIdQuery query, CancellationToken cancellationToken)
    {
        ProxySession? session = await dbContext.ProxySessions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.SessionId, cancellationToken);

        if (session is null)
        {
            return Result.Failure<ProxySessionDto>(
                Error.NotFound("ProxySession.NotFound", "Proxy session does not exist."));
        }

        return session.ToDto();
    }
}
