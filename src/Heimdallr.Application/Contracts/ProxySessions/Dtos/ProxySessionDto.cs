using Heimdallr.Domain.Enums;

namespace Heimdallr.Application.Contracts.ProxySessions.Dtos;

public sealed record ProxySessionDto(
    Guid Id,
    Guid UserId,
    Guid MeterId,
    int ProxyPort,
    SessionStatus Status,
    ClientIpPolicy ClientIpPolicy,
    string? PinnedClientIp,
    DateTimeOffset CreatedAt,
    DateTimeOffset? StartedAt,
    DateTimeOffset? LastActivityAt,
    DateTimeOffset? LeaseUntil,
    DateTimeOffset? EndedAt,
    long BytesFromClient,
    long BytesFromMeter,
    string? FaultReason);
