using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InternalApi.Endpoints;
using InternalApi.Services;
using InternalApi.Outbound;
using Shared.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var authority = builder.Configuration["InternalAuth:Authority"];
var audience = builder.Configuration["InternalAuth:Audience"];

builder.Services.AddSingleton<IAnimalDataService, AnimalDataService>();
builder.Services.AddScoped<IInternalAnimalService, InternalAnimalService>();
builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<BearerForwardingHandler>();
builder.Services.AddHttpClient("InternalPartnerApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["InternalPartners:BaseUrl"] ?? "https://localhost");
}).AddHttpMessageHandler<BearerForwardingHandler>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = authority ?? string.Empty;
        options.Audience = audience ?? string.Empty;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrWhiteSpace(authority),
            ValidateAudience = !string.IsNullOrWhiteSpace(audience),
            ValidateLifetime = true
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();

app.MapInternalAnimalEndpoints();
app.MapOpenApi("/openapi/internal.json");

app.Run();
