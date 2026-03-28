using System.Net;
using System.Net.Sockets;
using Heimdallr.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Heimdallr.Infrastructure.Proxying.Runtime;

public sealed class ProxySessionRuntime(
    Guid sessionId,
    int listeningPort,
    MeterEndpoint meterEndpoint,
    IProxyBridgeFactory proxyBridgeFactory,
    ILogger<ProxySessionRuntime> logger) : IProxySessionRuntime
{
    private readonly TcpListener _listener = new(IPAddress.Any, listeningPort);
    private readonly CancellationTokenSource _internalCts = new();

    private Task? _listenerTask;
    private volatile bool _started;

    public Guid SessionId { get; } = sessionId;
    public int ListeningPort { get; } = listeningPort;

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (_started)
        {
            throw new InvalidOperationException(
                $"Proxy session runtime for session '{SessionId}' is already started.");
        }

        _started = true;

        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _internalCts.Token);

        _listener.Start();

        logger.LogInformation(
            "Proxy session runtime started. SessionId={SessionId}, ListeningPort={ListeningPort}, TransportType={TransportType}",
            SessionId,
            ListeningPort,
            meterEndpoint.TransportType);

        _listenerTask = RunAcceptLoopAsync(linkedCts.Token);

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (!_started)
        {
            return;
        }

        await _internalCts.CancelAsync();

        try
        {
            _listener.Stop();
        }
        catch
        {
            // ignored
        }

        if (_listenerTask is not null)
        {
            await _listenerTask.WaitAsync(cancellationToken);
        }

        logger.LogInformation(
            "Proxy session runtime stopped. SessionId={SessionId}, ListeningPort={ListeningPort}",
            SessionId,
            ListeningPort);
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync();

        _internalCts.Dispose();
    }

    private async Task RunAcceptLoopAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync(cancellationToken);

                _ = Task.Run(
                    () => HandleClientAsync(client, cancellationToken),
                    CancellationToken.None);
            }
        }
        catch (OperationCanceledException)
        {
            // normal shutdown
        }
        catch (ObjectDisposedException)
        {
            // normal shutdown
        }
        catch (SocketException ex) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogDebug(
                ex,
                "Proxy listener socket stopped during cancellation. SessionId={SessionId}",
                SessionId);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unhandled proxy listener error. SessionId={SessionId}, ListeningPort={ListeningPort}",
                SessionId,
                ListeningPort);
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        string clientIp =
            (client.Client.RemoteEndPoint as IPEndPoint)?.Address.ToString()
            ?? "unknown";

        logger.LogInformation(
            "Incoming client connection accepted. SessionId={SessionId}, ListeningPort={ListeningPort}, ClientIp={ClientIp}",
            SessionId,
            ListeningPort,
            clientIp);

        try
        {
            IProxyBridge bridge = proxyBridgeFactory.Create(meterEndpoint);

            ProxyConnectionContext context = new(
                ClientIp: clientIp,
                ListeningPort: ListeningPort,
                CancellationToken: cancellationToken);

            ProxyTransferResult result = await bridge.RunAsync(client, context);

            logger.LogInformation(
                "Client connection finished. SessionId={SessionId}, ClientIp={ClientIp}, BytesFromClient={BytesFromClient}, BytesFromTarget={BytesFromTarget}, FaultReason={FaultReason}",
                SessionId,
                clientIp,
                result.BytesFromClient,
                result.BytesFromTarget,
                result.FaultReason);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Proxy bridge failed. SessionId={SessionId}, ListeningPort={ListeningPort}, ClientIp={ClientIp}",
                SessionId,
                ListeningPort,
                clientIp);

            client.Dispose();
        }
    }
}
