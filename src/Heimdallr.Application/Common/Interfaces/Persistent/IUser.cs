using Heimdallr.Domain.Entities;
using Heimdallr.Domain.Enums;

namespace Heimdallr.Application.Common.Interfaces.Persistent;

public interface IUser
{
    Guid Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    UserRole Role { get; set; }

    bool IsEnabled { get; set; }

    DateTimeOffset CreatedAt { get; set; }

    DateTimeOffset? LastLoginAt { get; set; }

    ICollection<UserIpRule> IpRules { get; set; }

    ICollection<ProxySession> Sessions { get; set; }
}
