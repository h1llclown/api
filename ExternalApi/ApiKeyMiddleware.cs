namespace ExternalApi.Authentication;

public sealed class ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private readonly string[] _validApiKeys =
        configuration.GetSection("ExternalAuth:ApiKeys").Get<string[]>() ?? [];

    public async Task InvokeAsync(HttpContext context)
    {
        var apiKey = context.Request.Headers["X-API-Key"].ToString();
        if (!_validApiKeys.Any(key =>
                !string.IsNullOrWhiteSpace(key) &&
                string.Equals(key, apiKey, StringComparison.Ordinal)))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "A valid X-API-Key is required." });
            return;
        }

        await next(context);
    }
}
