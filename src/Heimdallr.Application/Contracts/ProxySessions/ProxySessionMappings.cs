using Heimdallr.Application.Contracts.ProxySessions.Dtos;
using Heimdallr.Domain.Entities;

namespace Heimdallr.Application.Contracts.ProxySessions;

public static class ProxySessionMappings
{
    public static ProxySessionDto ToDto(this ProxySession session) =>
        new(
            session.Id,
            session.UserId,
            session.MeterId,
            session.ProxyPort,
            session.Status,
            session.ClientIpPolicy,
            session.PinnedClientIp,
            session.CreatedAt,
            session.StartedAt,
            session.LastActivityAt,
            session.LeaseUntil,
            session.EndedAt,
            session.BytesFromClient,
            session.BytesFromMeter,
            session.FaultReason);
}
