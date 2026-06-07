using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.Meters.Dtos;
using Heimdallr.Application.Contracts.Meters.Queries;
using Heimdallr.WebUI.Common.Constants;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages;

public partial class Meters : ComponentBase
{
    [Inject] 
    private IQueryHandler<GetMetersPageQuery, MetersPageDto> GetMetersHandler { get; set; }

    private int ItemsOnPage 
    { 
        get;
        set => field = Math.Clamp(value, 10, 250);
    } = 50;

    private int PageNumber
    {
        get;
        set => field = Math.Clamp(value, 1, Dto.PageCount);
    } = 1;

    private MetersPageDto Dto { get; set; } = new([], 1, 0, 0);

    protected override async Task OnInitializedAsync()
    {
        await GetMetersWithCurrentParameters();
    }

    private async Task GetMetersWithCurrentParameters()
    {
        using CancellationTokenSource cts = new(TimeSpan.FromSeconds(30));
        
        Result<MetersPageDto> result = await GetMetersHandler.Handle(new GetMetersPageQuery(ItemsOnPage, PageNumber), cts.Token);

        if (result.IsFailure)
        {
            // Handle error
            return;
        }
        
        Dto = result.Value;
    }

    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    private void CreateNewMeter()
    {
        NavigationManager.NavigateTo(PageRoute.MetersCreatePage);
    }
}

