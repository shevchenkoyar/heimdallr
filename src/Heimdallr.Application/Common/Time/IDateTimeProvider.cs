namespace Heimdallr.Application.Common.Time;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
