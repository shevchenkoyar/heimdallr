using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages;

[UsedImplicitly]
public partial class Home : AuthorizedComponent
{
    [Inject] public required NavigationManager NavigationManager { get; set; }
}
