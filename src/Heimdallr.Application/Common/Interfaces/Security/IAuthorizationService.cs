using Heimdallr.Application.Common.Monads;

namespace Heimdallr.Application.Common.Interfaces.Security;

public interface IAuthorizationService
{
    Task<Result> RegisterAsync(string login, string password, CancellationToken cancellationToken);
    
    Task<Result> LoginAsync(string login, string password, CancellationToken cancellationToken);
}
