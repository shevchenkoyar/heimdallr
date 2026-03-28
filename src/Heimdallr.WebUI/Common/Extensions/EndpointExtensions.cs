using Heimdallr.WebUI.Endpoints;

namespace Heimdallr.WebUI.Common.Extensions;

internal static class EndpointExtensions
{
    extension(WebApplication app)
    {
        public WebApplication MapEndpoints()
        {
            IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.Configure(app);
            }

            return app;
        }
    }
}
