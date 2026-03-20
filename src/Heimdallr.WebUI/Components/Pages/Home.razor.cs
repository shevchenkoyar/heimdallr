using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.Users.Queries.UserAuthorized;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages;

[UsedImplicitly]
public partial class Home : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required IQueryHandler<UserAuthorizedQuery, bool> UserAuthorizedHandler { get; set; }

    protected override async Task OnInitializedAsync()
    {
        using CancellationTokenSource cts = new(TimeSpan.FromSeconds(25));

        Result<bool> result = await UserAuthorizedHandler.Handle(new UserAuthorizedQuery(), cts.Token);

        if (result.IsFailure || !result.Value)
        {
            NavigationManager.NavigateTo("/auth");
        }
    }
}
