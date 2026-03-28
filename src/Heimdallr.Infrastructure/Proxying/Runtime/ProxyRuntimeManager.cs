using System.Collections.Concurrent;
using Heimdallr.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Heimdallr.Infrastructure.Proxying.Runtime;

public sealed class ProxyRuntimeManager(
    IProxyBridgeFactory proxyBridgeFactory,
    ILoggerFactory loggerFactory) : IProxyRuntimeManager
{
    private readonly ConcurrentDictionary<Guid, IProxySessionRuntime> _runtimes = new();

    public bool IsRunning(Guid sessionId) => _runtimes.ContainsKey(sessionId);

    public async Task StartAsync(
        Guid sessionId,
        int listeningPort,
        MeterEndpoint meterEndpoint,
        CancellationToken cancellationToken = default)
    {
        if (_runtimes.ContainsKey(sessionId))
        {
            throw new InvalidOperationException(
                $"Proxy runtime for session '{sessionId}' is already running.");
        }

        ProxySessionRuntime runtime = new(
            sessionId,
            listeningPort,
            meterEndpoint,
            proxyBridgeFactory,
            loggerFactory.CreateLogger<ProxySessionRuntime>());

        if (!_runtimes.TryAdd(sessionId, runtime))
        {
            await runtime.DisposeAsync();
            throw new InvalidOperationException(
                $"Failed to register proxy runtime for session '{sessionId}'.");
        }

        try
        {
            await runtime.StartAsync(cancellationToken);
        }
        catch
        {
            _runtimes.TryRemove(sessionId, out _);
            await runtime.DisposeAsync();
            throw;
        }
    }

    public async Task StopAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        if (!_runtimes.TryRemove(sessionId, out IProxySessionRuntime? runtime))
        {
            return;
        }

        await runtime.StopAsync(cancellationToken);
        await runtime.DisposeAsync();
    }
}
