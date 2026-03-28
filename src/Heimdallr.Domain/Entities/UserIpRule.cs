using Heimdallr.Domain.Enums;

namespace Heimdallr.Domain.Entities;

public sealed class UserIpRule
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public string IpOrCidr { get; set; }
    
    public IpRuleType Type { get; set; } = IpRuleType.SingleIp;

    public bool IsEnabled { get; set; } = true;

    public string? Comment { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
}
