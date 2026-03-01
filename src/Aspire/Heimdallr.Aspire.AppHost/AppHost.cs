IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("heimdallr-compose")
    .WithDashboard(dashboard =>
    {
        dashboard.WithHostPort(8080)
            .WithForwardedHeaders(enabled: true);
    });

IResourceBuilder<ParameterResource> pgUser = builder.AddParameter("heimdallr-pg-username", secret: true);
IResourceBuilder<ParameterResource> pgPass = builder.AddParameter("heimdallr-pg-password", secret: true);

IResourceBuilder<PostgresDatabaseResource> database = builder.AddPostgres("heimdallr-postgresql", pgUser, pgPass)
    .PublishAsDockerComposeService((_, service) =>
    {
        service.Name = "postgres";
    })
    .WithDataVolume("heimdallr-pgdata")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("heimdallr-db");

builder.AddProject<Projects.Heimdallr_WebUI>("heimdallr-web-ui")
    .WaitFor(database)
    .WithReference(database)
    .PublishAsDockerComposeService((_, service) =>
    {
        service.Name = "heimdallr-web-ui";
    });

await builder
    .Build()
    .RunAsync();
