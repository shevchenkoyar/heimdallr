using System.Net.Sockets;
using Heimdallr.Infrastructure.Proxying.Channels;
using Microsoft.Extensions.Logging;

namespace Heimdallr.Infrastructure.Proxying;

public sealed class TcpToTcpProxy(
    string targetHost,
    int targetPort,
    ILogger<TcpToTcpProxy> logger) : IProxyBridge
{
    public async Task<ProxyTransferResult> RunAsync(
        TcpClient client, 
        ProxyConnectionContext context)
    {
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        try
        {
            await using TcpClientChannel target = new(targetHost, targetPort);
            await target.OpenAsync(context.CancellationToken);

            await using NetworkStream clientStream = client.GetStream();

            logger.LogInformation(
                "TCP to TCP proxy started. ClientIp={ClientIp}, ListeningPort={ListeningPort}, Target={TargetHost}:{TargetPort}",
                context.ClientIp,
                context.ListeningPort,
                targetHost,
                targetPort);

            ProxyTransferResult result = await BidirectionalStreamPump.PumpAsync(
                clientStream,
                target.Input,
                context.CancellationToken);

            logger.LogInformation(
                "TCP to TCP proxy finished. ClientIp={ClientIp}, BytesFromClient={BytesFromClient}, BytesFromTarget={BytesFromTarget}, Fault={Fault}",
                context.ClientIp,
                result.BytesFromClient,
                result.BytesFromTarget,
                result.FaultReason);

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "TCP to TCP proxy failed. ClientIp={ClientIp}, ListeningPort={ListeningPort}, Target={TargetHost}:{TargetPort}",
                context.ClientIp,
                context.ListeningPort,
                targetHost,
                targetPort);

            return new ProxyTransferResult(
                0,
                0,
                startedAt,
                DateTimeOffset.UtcNow,
                ex.Message);
        }
        finally
        {
            client.Dispose();
        }
    }
}
