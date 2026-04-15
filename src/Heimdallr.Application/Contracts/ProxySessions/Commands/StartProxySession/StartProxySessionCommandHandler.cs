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

namespace Heimdallr.Application.Contracts.ProxySessions.Commands.StartProxySession;

[UsedImplicitly]
public sealed class StartProxySessionCommandHandler(
    IApplicationDbContext dbContext,
    IProxyPortAllocator proxyPortAllocator,
    IProxyRuntimeManager proxyRuntimeManager,
    IDateTimeProvider dateTimeProvider,
    IUserManager userManager)
    : ICommandHandler<StartProxySessionCommand, ProxySessionDto>
{
    public async Task<Result<ProxySessionDto>> Handle(StartProxySessionCommand command, CancellationToken cancellationToken)
    {
        DateTimeOffset now = dateTimeProvider.UtcNow;

        IUser? user = await userManager.FindUserByIdAsync(command.UserId);
        
        if (user is null)
        {
            return Result.Failure<ProxySessionDto>(Error.NotFound("User.NotFound", "User not found."));
        }

        if (!user.IsEnabled)
        {
            return Result.Failure<ProxySessionDto>(Error.Conflict("User.Disabled", "User is disabled."));
        }

        Meter? meter = await dbContext.Meters
            .Include(x => x.Endpoints)
            .FirstOrDefaultAsync(x => x.Id == command.MeterId, cancellationToken);

        if (meter is null)
        {
            return Result.Failure<ProxySessionDto>(
                Error.NotFound("Meter.NotFound", "Meter does not exist."));
        }

        if (!meter.IsEnabled)
        {
            return Result.Failure<ProxySessionDto>(
                Error.Problem("Meter.Disabled", "Meter is disabled."));
        }

        bool hasActiveSession = await dbContext.ProxySessions
            .AnyAsync(
                x => x.MeterId == command.MeterId &&
                     (x.Status == SessionStatus.Starting || x.Status == SessionStatus.Active),
                cancellationToken);

        if (hasActiveSession)
        {
            return Result.Failure<ProxySessionDto>(
                Error.Conflict("Meter.Reserved", "Meter is already reserved by another active session."));
        }

        MeterEndpoint? endpoint = meter.Endpoints
            .FirstOrDefault(x => x is { IsEnabled: true, IsPrimary: true });

        if (endpoint is null)
        {
            return Result.Failure<ProxySessionDto>(
                Error.Problem("Meter.EndpointNotFound", "Primary enabled meter endpoint was not found."));
        }

        int allocatedPort = await proxyPortAllocator.ReserveFreePortAsync(cancellationToken);

        ProxySession session = new()
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            MeterId = command.MeterId,
            ProxyPort = allocatedPort,
            Status = SessionStatus.Starting,
            ClientIpPolicy = command.ClientIpPolicy,
            PinnedClientIp = null,
            CreatedAt = now,
            LeaseUntil = now.Add(command.LeaseDuration)
        };

        dbContext.ProxySessions.Add(session);
        await dbContext.SaveChangesAsync(cancellationToken);

        try
        {
            await proxyRuntimeManager.StartAsync(
                session.Id,
                session.ProxyPort,
                endpoint,
                cancellationToken);

            await proxyPortAllocator.MarkPortInUseAsync(session.ProxyPort, cancellationToken);

            session.Status = SessionStatus.Active;
            session.StartedAt = now;
            session.LastActivityAt = now;

            await dbContext.SaveChangesAsync(cancellationToken);

            return session.ToDto();
        }
        catch (Exception ex)
        {
            session.Status = SessionStatus.Faulted;
            session.FaultReason = ex.Message;
            session.EndedAt = dateTimeProvider.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);
            await proxyPortAllocator.ReleasePortAsync(session.ProxyPort, cancellationToken);

            return Result.Failure<ProxySessionDto>(Error.Failure("Meter.Proxy.Error", ex.Message));
        }
    }
}
