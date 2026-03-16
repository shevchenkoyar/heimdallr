namespace Heimdallr.Application.Common.Interfaces.Security;


/// <summary>
/// put raw string password and get hashed password
/// </summary>
public interface IPasswordHasher
{
    ValueTask<string> HashAsync(string login, string password, CancellationToken cancellationToken);
}
