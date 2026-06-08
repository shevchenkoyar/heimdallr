using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages.Meters;

public partial class EditMeter : ComponentBase
{
    [Parameter] public required Guid MeterId { get; set; }
    
    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }
}
