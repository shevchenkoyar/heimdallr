using System.IO.Ports;
using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Heimdallr.Infrastructure.Proxying;

public sealed class ProxyBridgeFactory(ILoggerFactory loggerFactory) : IProxyBridgeFactory
{
    public IProxyBridge Create(MeterEndpoint meterEndpoint)
    {
        return meterEndpoint.TransportType switch
        {
            TransportType.Tcp => CreateTcpToTcp(meterEndpoint),
            TransportType.SerialOverTcp => CreateTcpToSerial(meterEndpoint),
            _ => throw new NotSupportedException(
                $"Transport type '{meterEndpoint.TransportType}' is not supported.")
        };
    }

    private IProxyBridge CreateTcpToTcp(MeterEndpoint meterEndpoint)
    {
        if (string.IsNullOrWhiteSpace(meterEndpoint.Host))
        {
            throw new InvalidOperationException("Meter endpoint host is not configured.");
        }

        if (meterEndpoint.Port <= 0)
        {
            throw new InvalidOperationException("Meter endpoint port is not configured.");
        }

        return new TcpToTcpProxy(
            meterEndpoint.Host,
            meterEndpoint.Port,
            loggerFactory.CreateLogger<TcpToTcpProxy>());
    }

    private IProxyBridge CreateTcpToSerial(MeterEndpoint meterEndpoint)
    {
        var settings = SerialSettings.FromJson(meterEndpoint.MetaJson);

        return new TcpToSerialProxy(
            settings.PortName,
            settings.BaudRate,
            settings.Parity,
            settings.DataBits,
            settings.StopBits,
            loggerFactory.CreateLogger<TcpToSerialProxy>());
    }

    private sealed record SerialSettings(
        string PortName,
        int BaudRate,
        Parity Parity,
        int DataBits,
        StopBits StopBits)
    {
        public static SerialSettings FromJson(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidOperationException(
                    "Serial transport requires MetaJson with serial settings.");
            }

            SerialSettings? settings = System.Text.Json.JsonSerializer.Deserialize<SerialSettings>(json);
            return settings ?? throw new InvalidOperationException("Failed to deserialize serial settings.");
        }
    }
}
