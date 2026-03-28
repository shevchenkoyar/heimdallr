using System.Net.Sockets;

namespace Heimdallr.Infrastructure.Proxying;

public interface IProxyBridge
{
    Task<ProxyTransferResult> RunAsync(
        TcpClient client,
        ProxyConnectionContext context);
}
