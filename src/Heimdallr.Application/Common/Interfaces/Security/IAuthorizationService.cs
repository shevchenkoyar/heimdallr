using Heimdallr.Application.Common.Monads;
using Heimdallr.Domain.Entities;

namespace Heimdallr.Application.Common.Interfaces.Security;

public interface IAuthorizationService
{
    Task<Result> RegisterAsync(string login, string firstName, string lastName, string password,
        CancellationToken token);
    
    Task<Result> LoginAsync(string login, string password, CancellationToken cancellationToken);
}
