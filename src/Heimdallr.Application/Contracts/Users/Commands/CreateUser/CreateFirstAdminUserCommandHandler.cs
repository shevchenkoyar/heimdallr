using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using JetBrains.Annotations;

namespace Heimdallr.Application.Contracts.Users.Commands.CreateUser;

[UsedImplicitly]
internal class CreateFirstAdminUserCommandHandler(IAuthorizationService authorizationService) : ICommandHandler<CreateFirstAdminUserCommand>
{
    private const string AdminLogin = "Admin";
    private const string AdminPassword = "Admin12345!";
    
    public async Task<Result> Handle(CreateFirstAdminUserCommand command, CancellationToken cancellationToken)
    {
        await authorizationService.RegisterAsync(AdminLogin, AdminPassword, cancellationToken);
        return Result.Success();
    }
}
