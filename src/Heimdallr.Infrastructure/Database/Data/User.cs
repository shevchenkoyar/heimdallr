using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Heimdallr.Infrastructure.Database.Data;

internal class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? LastLoginAt { get; set; }

    public ICollection<UserIpRule> IpRules { get; set; } = new List<UserIpRule>();

    public ICollection<ProxySession> Sessions { get; set; } = new List<ProxySession>();

    private User() { }

    public static User Create(string username, string firstName, string lastName)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            UserName = username,
            CreatedAt = DateTimeOffset.UtcNow,
            IsEnabled = true,
            Role = UserRole.User,
            FirstName = firstName,
            LastName = lastName
        };
    }
}
