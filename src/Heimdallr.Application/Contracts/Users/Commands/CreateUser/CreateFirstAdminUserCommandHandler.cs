using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Contracts.Users.Commands.CreateUser;

[UsedImplicitly]
internal class CreateFirstAdminUserCommandHandler(IDbContext db, IPasswordHasher hasher) : ICommandHandler<CreateFirstAdminUserCommand>
{
    private const string AdminPassword = "Admin";
    
    public async Task<Result> Handle(CreateFirstAdminUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await db.Users.AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            string passwordHash = await hasher.HashAsync(AdminPassword, cancellationToken);
            var firstAdminUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = "Admin",
                PasswordHash = passwordHash,
                FirstName = null,
                LastName = null,
                Role = UserRole.Admin,
                IsEnabled = true,
                CreatedAt = DateTimeOffset.UtcNow,
                LastLoginAt = null
            };
            await db.Users.AddAsync(firstAdminUser, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
        
        return Result.Success();
    }
}
