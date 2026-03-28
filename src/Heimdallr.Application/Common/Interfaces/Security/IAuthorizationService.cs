using System.Security.Claims;
using Heimdallr.Application.Common.Monads;

namespace Heimdallr.Application.Common.Interfaces.Security;

public interface IAuthorizationService
{
    Task<Result> RegisterAsync(string login, string firstName, string lastName, string password,
        CancellationToken cancellationToken);

    Task<Result<List<Claim>>> LoginAsync(string login, string password, CancellationToken cancellationToken);
}
