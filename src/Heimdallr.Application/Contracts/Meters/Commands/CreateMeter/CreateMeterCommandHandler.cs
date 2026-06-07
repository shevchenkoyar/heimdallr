using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Domain.Entities;

namespace Heimdallr.Application.Contracts.Meters.Commands.CreateMeter;

internal sealed class CreateMeterCommandHandler(
    IApplicationDbContext dbContext
    ) : ICommandHandler<CreateMeterCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMeterCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = Validate(command);
        
        if (validationResult.IsFailure)
        {
            return Result.Failure<Guid>(validationResult.Error);
        }
        
        var newMeter = Meter.Create(command.MeterName, command.Model, command.SerialNumber);
        
        await dbContext.Meters.AddAsync(newMeter, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return newMeter.Id;
    }

    private static Result Validate(CreateMeterCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.MeterName))
        {
            return Result.Failure(Error.Failure("Meter.Validation", "Meter name cannot be empty."));
        }
        
        if (command.MeterName.Length > 200)
        {
            return Result.Failure(Error.Failure("Meter.Validation", "Meter name cannot be longer than 200 characters."));
        }
        
        if (command.SerialNumber?.Length > 200)
        {
            return Result.Failure(Error.Failure("Meter.Validation", "Meter serial number cannot be longer than 200 characters."));
        }
        
        if (command.Model?.Length > 200)
        {
            return Result.Failure(Error.Failure("Meter.Validation", "Meter model cannot be longer than 200 characters."));
        }
        
        return Result.Success();
    }
}
