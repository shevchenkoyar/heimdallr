using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.Users.Commands.Login;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages;

public partial class Authorization : ComponentBase
{
    private string Login { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private string ErrorMessage { get; set; } = string.Empty;
    private bool IsErrorShown { get; set; }
    private bool IsAuthorizedShown { get; set; }

    private void CloseError()
    {
        IsErrorShown = false;
    }

    private async Task Submit()
    {
        using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        IsErrorShown = false;
        ErrorMessage = string.Empty;
        
        Result authResult = await Authorize.Handle(new LoginCommand(Login, Password), source.Token);

        IsAuthorizedShown = authResult.IsSuccess;

        IsErrorShown = authResult.IsFailure;

        if (IsErrorShown)
        {
            ErrorMessage = authResult.Error.Description;
        }
    }
}
