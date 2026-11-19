using ExternalApi.Authentication;
using ExternalApi.Endpoints;
using ExternalApi.Stores;
using Shared.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAnimalStore, ExternalAnimalStore>();
builder.Services.AddOpenApi();
builder.Services.AddTransient<Outbound.ApiKeyDelegatingHandler>();
builder.Services.AddHttpClient("PartnerApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalPartners:BaseUrl"] ?? "https://localhost");
}).AddHttpMessageHandler<Outbound.ApiKeyDelegatingHandler>();

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();
app.MapExternalAnimalEndpoints();
app.MapOpenApi("/openapi/external.json");

app.Run();
