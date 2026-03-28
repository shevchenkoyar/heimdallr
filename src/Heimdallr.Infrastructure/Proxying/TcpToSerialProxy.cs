using System.IO.Ports;
using System.Net.Sockets;
using Heimdallr.Infrastructure.Proxying.Channels;
using Microsoft.Extensions.Logging;

namespace Heimdallr.Infrastructure.Proxying;

public sealed class TcpToSerialProxy(
    string serialPortName,
    int baudRate,
    Parity parity,
    int dataBits,
    StopBits stopBits,
    ILogger<TcpToSerialProxy> logger) : IProxyBridge
{
    public async Task<ProxyTransferResult> RunAsync(
        TcpClient client, 
        ProxyConnectionContext context)
    {
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        try
        {
            await using SerialPortChannel target = new(
                serialPortName,
                baudRate,
                parity,
                dataBits,
                stopBits);
            await target.OpenAsync(context.CancellationToken);

            await using NetworkStream clientStream = client.GetStream();

            logger.LogInformation(
                "TCP toSERIAL proxy started. ClientIp={ClientIp}, ListeningPort={ListeningPort}, SerialPort={SerialPort}",
                context.ClientIp,
                context.ListeningPort,
                serialPortName);

            ProxyTransferResult result = await BidirectionalStreamPump.PumpAsync(
                clientStream,
                target.Input,
                context.CancellationToken);

            logger.LogInformation(
                "TCP to SERIAL proxy finished. ClientIp={ClientIp}, BytesFromClient={BytesFromClient}, BytesFromTarget={BytesFromTarget}, Fault={Fault}",
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
                "TCP->SERIAL proxy failed. ClientIp={ClientIp}, ListeningPort={ListeningPort}, SerialPort={SerialPort}",
                context.ClientIp,
                context.ListeningPort,
                serialPortName);

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
