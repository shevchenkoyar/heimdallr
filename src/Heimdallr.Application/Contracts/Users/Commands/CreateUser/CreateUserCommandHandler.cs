using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Domain.Entities;
using JetBrains.Annotations;

namespace Heimdallr.Application.Contracts.Users.Commands.CreateUser;

[UsedImplicitly]
internal class CreateUserCommandHandler(IDbContext db, IPasswordHasher hasher) : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        string hash = await hasher.HashAsync(command.Username, command.Password, cancellationToken);
        
        var newUser = User.Create(command.Username, hash);
        
        await db.Users.AddAsync(newUser, cancellationToken);
        
        return Result.Success();
    }
}
