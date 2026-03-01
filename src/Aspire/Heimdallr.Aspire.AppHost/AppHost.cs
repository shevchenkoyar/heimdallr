IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> pgUser = builder.AddParameter("heimdallr-pg-username", secret: true);
IResourceBuilder<ParameterResource> pgPass = builder.AddParameter("heimdallr-pg-password", secret: true);

IResourceBuilder<PostgresDatabaseResource> database = builder.AddPostgres("heimdallr-postgresql", pgUser, pgPass)
    .WithDataVolume("heimdallr-pgdata")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("heimdallr-db");

builder.AddProject<Projects.Heimdallr_WebUI>("heimdallr-web-ui")
    .WaitFor(database)
    .WithReference(database);

await builder
    .Build()
    .RunAsync();
