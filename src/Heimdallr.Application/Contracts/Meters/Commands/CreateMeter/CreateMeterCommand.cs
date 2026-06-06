using Heimdallr.Application.Common.Interfaces.Contracts;

namespace Heimdallr.Application.Contracts.Meters.Commands.CreateMeter;

public sealed record CreateMeterCommand(
    string MeterName,
    string? Model,
    string? SerialNumber
    ) : ICommand<Guid>;
