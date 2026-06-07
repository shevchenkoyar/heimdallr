using Microsoft.AspNetCore.Components;

namespace Heimdallr.WebUI.Components.Navigation;

public partial class NavMenu : ComponentBase
{
    private IReadOnlyCollection<NavigationItem> Items { get; } =
    [
        new("Счетчики", "meters")
    ];
    
    private sealed class NavigationItem(string title, string url)
    {
        public string Title { get; } = title;
        public string Url { get; } = url;
    }
}
