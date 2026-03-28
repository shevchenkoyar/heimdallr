using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Common.Input;

public partial class Password : ComponentBase
{
    [Parameter]
    public string? Value
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    private InputType PasswordInputType { get; set; } = InputType.Password;

    private void ShowPassword()
    {
        PasswordInputType = PasswordInputType switch
        {
            InputType.Password => InputType.Text,
            InputType.Text => InputType.Password,
            _ => PasswordInputType
        };
    }

    private enum InputType
    {
        Text,
        Password
    }
}
