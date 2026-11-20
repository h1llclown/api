using Microsoft.AspNetCore.Http.HttpResults;
using ExternalApi.Models;
using ExternalApi.Services;

namespace ExternalApi.Endpoints;

public static class ExternalAnimalEndpoints
{
    public static RouteGroupBuilder MapExternalAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var profiles = app.MapGroup("/api/external/animals")
            .WithTags("External Animal Profiles");

        profiles.MapGet("/", (IExternalAnimalService service, CancellationToken cancellationToken) =>
                TypedResults.Ok(service.List()))
            .WithName("ExternalListAnimals")
            .WithSummary("List animals through the external API")
            .WithDescription("Returns public adoption-profile data. Requires an X-API-Key header.")
            .Produces<IReadOnlyCollection<AnimalProfileResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        profiles.MapGet("/{animalId:guid}",
            Results<Ok<AnimalProfileResponse>, NotFound>
            (Guid animalId, IExternalAnimalService service, CancellationToken cancellationToken) =>
                service.Get(animalId) is { } profile
                    ? TypedResults.Ok(profile)
                    : TypedResults.NotFound())
            .WithName("ExternalGetAnimal")
            .WithSummary("Get an animal profile through the external API")
            .WithDescription("Returns public adoption-profile data. Requires an X-API-Key header.")
            .Produces<AnimalProfileResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

        return profiles;
    }
}
