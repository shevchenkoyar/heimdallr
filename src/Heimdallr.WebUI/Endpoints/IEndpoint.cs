namespace Heimdallr.WebUI.Endpoints;

public interface IEndpoint
{
    string Endpoint { get; }

    void Configure(WebApplication app);
}
