using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace Heimdallr.Infrastructure.Proxying;

public sealed class ProxyPortListener(
    int listeningPort,
    IProxyBridge bridge,
    ILogger<ProxyPortListener> logger)
{
    private readonly TcpListener _listener = new(IPAddress.Any, listeningPort);

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        _listener.Start();
        
        logger.LogInformation("Proxy listener started on port {Port}", listeningPort);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync(cancellationToken);

                _ = Task.Run(async () =>
                {
                    string clientIp =
                        (client.Client.RemoteEndPoint as IPEndPoint)?.Address.ToString()
                        ?? "unknown";

                    ProxyConnectionContext context = new(
                        ClientIp: clientIp,
                        ListeningPort: listeningPort,
                        CancellationToken: cancellationToken);

                    await bridge.RunAsync(client, context);
                }, cancellationToken);
            }
        }
        finally
        {
            _listener.Stop();
            logger.LogInformation("Proxy listener stopped on port {Port}", listeningPort);
        }
    }
}
