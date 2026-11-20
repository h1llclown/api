using InternalApi.Models;
using Shared.Models;
using Shared.Services;

namespace InternalApi.Services;

internal sealed class InternalAnimalService(IAnimalDataService animalDataService) : IInternalAnimalService
{
    public IReadOnlyCollection<AnimalRecordResponse> List() =>
        animalDataService.List()
        .Select(ToResponse)
        .ToArray();

    public AnimalRecordResponse? Get(Guid id) =>
        animalDataService.Get(id) is { } animal
            ? ToResponse(animal)
            : null;

    public AnimalRecordResponse Create(RegisterAnimalRequest request)
    {
        var animal = animalDataService.Create(
            new CreateAnimalDataRequest(
                request.RegisteredName,
                request.Species,
                request.Category,
                request.BirthDate,
                request.Breed,
                request.Attributes,
                request.MedicalNotes,
                request.PublicDescription,
                request.AvailableForAdoption));

        return ToResponse(animal);
    }

    public bool Delete(Guid id) => animalDataService.Delete(id);

    private static AnimalRecordResponse ToResponse(AnimalDataRecord animal) =>
        new(
            animal.Id,
            animal.Name,
            animal.Species,
            animal.Category,
            animal.BirthDate,
            animal.Breed,
            animal.Attributes,
            animal.MedicalNotes,
            animal.PublicDescription,
            animal.AvailableForAdoption,
            animal.RegisteredAt,
            animal.LastUpdatedAt);
}
