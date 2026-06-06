namespace Heimdallr.Application.Contracts.Meters.Dtos;

public class MeterProxySessionDto(
    Guid id,
    string userName,
    string meterName,
    string? pinnedClientIp,
    DateTimeOffset? lastActivityAt,
    DateTimeOffset? leaseUntil,
    long bytesFromClient,
    long bytesFromMeter)
{
    public Guid Id { get; } = id;

    public string UserName { get; } = userName;

    public string MeterName { get; } = meterName;

    public string? PinnedClientIp { get; } = pinnedClientIp;

    public DateTimeOffset? LastActivityAt { get; } = lastActivityAt;

    public DateTimeOffset? LeaseUntil { get; } = leaseUntil;

    public long BytesFromClient { get; } = bytesFromClient;

    public long BytesFromMeter { get; } = bytesFromMeter;
}
