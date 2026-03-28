using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Common.Runtime;
using Heimdallr.Application.Common.Time;
using Heimdallr.Application.Contracts.ProxySessions.Dtos;
using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Contracts.ProxySessions.Commands.StopProxySession;

[UsedImplicitly]
public sealed class StopProxySessionCommandHandler(
    IApplicationDbContext dbContext,
    IProxyRuntimeManager proxyRuntimeManager,
    IProxyPortAllocator proxyPortAllocator,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<StopProxySessionCommand, ProxySessionDto>
{
    public async Task<Result<ProxySessionDto>> Handle(StopProxySessionCommand command, CancellationToken cancellationToken)
    {
        ProxySession? session = await dbContext.ProxySessions
            .SingleOrDefaultAsync(x => x.Id == command.SessionId, cancellationToken);

        if (session is null)
        {
            return Result.Failure<ProxySessionDto>(
                Error.NotFound("ProxySession.NotFound", "Proxy session does not exist."));
        }
        
        if (session.UserId != command.UserId)
        {
            return Result.Failure<ProxySessionDto>(
                Error.Failure("ProxySession.Unauthorized", "User cannot stop someone else's session."));
        }

        if (session.Status is SessionStatus.Stopped or SessionStatus.Expired or SessionStatus.Faulted)
        {
            return session.ToDto();
        }

        await proxyRuntimeManager.StopAsync(session.Id, cancellationToken);
        await proxyPortAllocator.ReleasePortAsync(session.ProxyPort, cancellationToken);

        session.Status = SessionStatus.Stopped;
        session.EndedAt = dateTimeProvider.UtcNow;
        session.LastActivityAt ??= session.EndedAt;

        await dbContext.SaveChangesAsync(cancellationToken);

        return session.ToDto();
    }
}
