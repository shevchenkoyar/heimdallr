using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using JetBrains.Annotations;

namespace Heimdallr.Application.Contracts.Users.Commands.CreateUser;

[UsedImplicitly]
internal class CreateFirstAdminUserCommandHandler(IAuthorizationService authorizationService) : ICommandHandler<CreateFirstAdminUserCommand>
{
    private const string AdminCreds = "Admin";
    
    public async Task<Result> Handle(CreateFirstAdminUserCommand command, CancellationToken cancellationToken)
    {
        await authorizationService.RegisterAsync(AdminCreds, AdminCreds, cancellationToken);
        return Result.Success();
    }
}
