using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Heimdallr.WebUI.Components.Pages;

[UsedImplicitly]
public partial class Authorization : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required IJSRuntime Js { get; set; }
    
    private string Login { get; set; } = "Admin";
    
    private string Password { get; set; } = "Admin12345!";
    private async Task Submit()
    {
        bool result = await Js.InvokeAsync<bool>(
            "window.auth",
            Login,
            Password
        );

        if (result)
        {
            NavigationManager.NavigateTo("/", true);
        }
    }
}
