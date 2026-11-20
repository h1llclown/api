using Microsoft.AspNetCore.Http.HttpResults;
using InternalApi.Models;
using InternalApi.Services;

namespace InternalApi.Endpoints;

public static class InternalAnimalEndpoints
{
    public static RouteGroupBuilder MapInternalAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var records = app.MapGroup("/api/internal/animals")
            .WithTags("Internal Animals")
            .RequireAuthorization();

        records.MapGet("/", (IInternalAnimalService service, CancellationToken cancellationToken) =>
                TypedResults.Ok(service.List()))
            .WithName("InternalListAnimals")
            .WithSummary("List animal registry records")
            .WithDescription("Returns registry data for authorized internal clients.")
            .Produces<IReadOnlyCollection<AnimalRecordResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);

        records.MapGet("/{animalId:guid}",
            Results<Ok<AnimalRecordResponse>, NotFound>
            (Guid animalId, IInternalAnimalService service, CancellationToken cancellationToken) =>
                service.Get(animalId) is { } record
                    ? TypedResults.Ok(record)
                    : TypedResults.NotFound())
            .WithName("InternalGetAnimal")
            .WithSummary("Get an animal registry record")
            .WithDescription("Returns registry and care data for an authorized internal client.")
            .Produces<AnimalRecordResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);

        records.MapPost("/", (RegisterAnimalRequest request, IInternalAnimalService service, CancellationToken cancellationToken) =>
        {
            var record = service.Create(request);
            return TypedResults.Created($"/api/internal/animals/{record.Id}", record);
        })
            .WithName("InternalCreateAnimal")
            .WithSummary("Register an animal")
            .WithDescription("Creates a registry record using the internal registry data model.")
            .Produces<AnimalRecordResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesValidationProblem();

        records.MapDelete("/{animalId:guid}",
            Results<NoContent, NotFound>
            (Guid animalId, IInternalAnimalService service, CancellationToken cancellationToken) =>
                service.Delete(animalId) ? TypedResults.NoContent() : TypedResults.NotFound())
            .WithName("InternalDeleteAnimal")
            .WithSummary("Delete an animal registry record")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);

        return records;
    }
}
