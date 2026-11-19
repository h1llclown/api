var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.api>("external-api")
	.WithExternalHttpEndpoints();

builder.AddProject<Projects.InternalApi>("internal-api");

builder.Build().Run();
