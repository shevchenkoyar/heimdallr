using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components;

[Authorize]
public abstract class AuthorizedComponent : ComponentBase;
