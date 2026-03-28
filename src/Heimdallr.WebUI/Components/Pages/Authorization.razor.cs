using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.Users.Commands.Login;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages;

public partial class Authorization : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private string Login { get; set; } = "Admin";
    private string Password { get; set; } = "Admin12345!";
    private string ErrorMessage { get; set; } = string.Empty;
    private bool IsErrorShown { get; set; }
    private bool IsAuthorized { get; set; }
    
    private async Task Submit()
    {
        using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        IsErrorShown = false;
        ErrorMessage = string.Empty;
        
        Result authResult = await Authorize.Handle(new LoginCommand(Login, Password), source.Token);

        IsAuthorized = authResult.IsSuccess;

        IsErrorShown = authResult.IsFailure;

        if (IsErrorShown)
        {
            ErrorMessage = authResult.Error.Description;
        }

        if (IsAuthorized)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
