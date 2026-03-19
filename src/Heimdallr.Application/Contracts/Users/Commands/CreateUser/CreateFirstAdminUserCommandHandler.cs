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
    private const string AdminCreds = "Admin";
    
    public async Task<Result> Handle(CreateFirstAdminUserCommand command, CancellationToken cancellationToken)
    {
        db.DomainUsers.RemoveRange(db.DomainUsers);
        await db.SaveChangesAsync(cancellationToken);
        User? user = await db.DomainUsers.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
 
        if (user == null)
        {
            string passwordHash = await hasher.HashAsync(AdminCreds, AdminCreds, cancellationToken);
            var firstAdminUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = AdminCreds,
                PasswordHash = passwordHash,
                FirstName = null,
                LastName = null,
                Role = UserRole.Admin,
                IsEnabled = true,
                CreatedAt = DateTimeOffset.UtcNow,
                LastLoginAt = null
            };
            await db.DomainUsers.AddAsync(firstAdminUser, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
        
        return Result.Success();
    }
}
