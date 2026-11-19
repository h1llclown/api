using Shared.Models;
using Shared.Stores;

namespace ExternalApi.Endpoints;

public static class ExternalAnimalEndpoints
{
    public static RouteGroupBuilder MapExternalAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var animals = app.MapGroup("/api/external/animals")
            .WithTags("External Animals");

        animals.MapGet("/", (IAnimalStore store, CancellationToken cancellationToken) =>
                Results.Ok(store.List()))
            .WithName("ExternalListAnimals")
            .WithSummary("List animals through the external API")
            .WithDescription("Requires an external API key.");

        animals.MapGet("/{animalId:guid}",
            (Guid animalId, IAnimalStore store, CancellationToken cancellationToken) =>
                store.Get(animalId) is { } animal
                    ? Results.Ok(animal)
                    : Results.NotFound())
            .WithName("ExternalGetAnimal")
            .WithSummary("Get an animal through the external API")
            .WithDescription("Requires an external API key.");

        return animals;
    }
}
