using Heimdallr.Domain.Entities;

namespace Heimdallr.Infrastructure.Proxying.Runtime;

public interface IProxyRuntimeManager
{
    Task StartAsync(
        Guid sessionId,
        int listeningPort,
        MeterEndpoint meterEndpoint,
        CancellationToken cancellationToken = default);

    Task StopAsync(Guid sessionId, CancellationToken cancellationToken = default);

    bool IsRunning(Guid sessionId);
}
