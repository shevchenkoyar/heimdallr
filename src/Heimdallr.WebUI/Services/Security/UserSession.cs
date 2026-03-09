using Heimdallr.Application.Common.Interfaces.Security;

namespace Heimdallr.WebUI.Services.Security;

internal class UserSession : IUserSession
{
    public Guid UserId { get; set; } = Guid.Empty;
}
