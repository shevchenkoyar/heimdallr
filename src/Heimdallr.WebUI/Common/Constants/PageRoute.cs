namespace Heimdallr.WebUI.Common.Constants;

internal sealed class PageRoute
{
    public const string HomePage = "";

    public const string AuthPage = "auth";
    
    public const string MetersPage = "meters";
    public const string MetersCreatePage = "meters/create";

    public const string MeterEditPage = "meters/edit/{meterId:guid}";
}
