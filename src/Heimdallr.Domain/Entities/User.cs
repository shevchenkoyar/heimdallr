using Heimdallr.Domain.Enums;

namespace Heimdallr.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? LastLoginAt { get; set; }

    public ICollection<UserIpRule> IpRules { get; set; } = new List<UserIpRule>();
    
    public ICollection<ProxySession> Sessions { get; set; } = new List<ProxySession>();

    public static User Create(Guid identityId, string username)
    {
        return new User
        {
            Id = identityId,
            UserName = username,
            CreatedAt = DateTimeOffset.UtcNow,
            IsEnabled = true,
            Role = UserRole.User
        };
    }
}
