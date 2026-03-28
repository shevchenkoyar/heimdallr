using System.Security.Claims;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using JetBrains.Annotations;

namespace Heimdallr.Application.Contracts.Users.Commands.Login;

[UsedImplicitly]
internal class LoginCommandHandler(IAuthorizationService authorizationService)
    : ICommandHandler<LoginCommand, List<Claim>>
{
    public async Task<Result<List<Claim>>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        return await authorizationService.LoginAsync(command.Login, command.Password, cancellationToken);
    }
}
