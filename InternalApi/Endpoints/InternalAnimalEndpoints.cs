using Shared.Models;
using Shared.Stores;

namespace InternalApi.Endpoints;

public static class InternalAnimalEndpoints
{
    public static RouteGroupBuilder MapInternalAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var animals = app.MapGroup("/api/internal/animals")
            .WithTags("Internal Animals")
            .RequireAuthorization();

        animals.MapGet("/", (IAnimalStore store, CancellationToken cancellationToken) =>
                Results.Ok(store.List()))
            .WithName("InternalListAnimals")
            .WithSummary("List all animals internally");

        animals.MapGet("/{animalId:guid}",
            (Guid animalId, IAnimalStore store, CancellationToken cancellationToken) =>
                store.Get(animalId) is { } animal
                    ? Results.Ok(animal)
                    : Results.NotFound())
            .WithName("InternalGetAnimal")
            .WithSummary("Get an animal internally");

        animals.MapPost("/", (CreateAnimalRequest request, IAnimalStore store, CancellationToken cancellationToken) =>
        {
            var animal = store.Create(request);
            return Results.Created($"/api/internal/animals/{animal.Id}", animal);
        })
            .WithName("InternalCreateAnimal")
            .WithSummary("Create an animal internally");

        animals.MapDelete("/{animalId:guid}",
            (Guid animalId, IAnimalStore store, CancellationToken cancellationToken) =>
                store.Delete(animalId) ? Results.NoContent() : Results.NotFound())
            .WithName("InternalDeleteAnimal")
            .WithSummary("Delete an animal internally");

        return animals;
    }
}
