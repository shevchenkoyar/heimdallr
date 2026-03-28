using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages;

[UsedImplicitly]
public partial class Home : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        
    }
}
