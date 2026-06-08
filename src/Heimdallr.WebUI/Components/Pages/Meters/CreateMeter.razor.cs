using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.Meters.Commands.CreateMeter;
using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Pages.Meters;

public partial class CreateMeter : ComponentBase
{
    public string MeterName { get; set; } = "";

    private bool _isMeterNameValid = true;

    private string _errorMessage = "";

    [Inject] public required ICommandHandler<CreateMeterCommand, Guid> CreateMeterCommand { get; set; }

    private async Task Create()
    {
        Result<Guid> result = await CreateMeterCommand.Handle(new CreateMeterCommand(MeterName), CancellationToken.None);
        
        if (result.IsFailure)
        {
            _isMeterNameValid = false;
            _errorMessage = result.Error.Description;
        }
    }

    private void OnValueChanged(string obj) => MeterName = obj;
}
