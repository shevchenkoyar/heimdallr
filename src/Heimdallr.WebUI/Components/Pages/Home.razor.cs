using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Heimdallr.WebUI.Components.Pages;

[UsedImplicitly]
public partial class Home : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required AuthenticationStateProvider AuthStateProvider { get; set; }

    private AuthenticationState AuthenticationState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        AuthenticationState = await AuthStateProvider.GetAuthenticationStateAsync();

        if (AuthenticationState.User == null || AuthenticationState.User.Identity == null ||
            !AuthenticationState.User.Identity.IsAuthenticated)
        {
            NavigationManager.NavigateTo("/auth");
        }
    }
    
}
