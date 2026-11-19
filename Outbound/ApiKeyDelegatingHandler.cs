namespace Outbound;

public sealed class ApiKeyDelegatingHandler(IConfiguration configuration) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var apiKey = configuration["ExternalPartners:ApiKey"];
        if (!string.IsNullOrWhiteSpace(apiKey))
            request.Headers.TryAddWithoutValidation("X-API-Key", apiKey);

        return base.SendAsync(request, cancellationToken);
    }
}
