using ExternalApi.Authentication;
using ExternalApi.Endpoints;
using ExternalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IExternalAnimalService, ExternalAnimalService>();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddTransient<Outbound.ApiKeyDelegatingHandler>();
builder.Services.AddHttpClient("PartnerApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalPartners:BaseUrl"] ?? "https://localhost");
}).AddHttpMessageHandler<Outbound.ApiKeyDelegatingHandler>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();
app.MapExternalAnimalEndpoints();
app.MapOpenApi("/openapi/external.json");

app.Run();
