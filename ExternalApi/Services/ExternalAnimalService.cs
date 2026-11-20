using ExternalApi.Models;
using Shared.Models;
using Shared.Services;

namespace ExternalApi.Services;

internal sealed class ExternalAnimalService(IAnimalDataService animalDataService) : IExternalAnimalService
{
    public IReadOnlyCollection<AnimalProfileResponse> List() =>
        animalDataService.List()
        .Select(ToResponse)
        .ToArray();

    public AnimalProfileResponse? Get(Guid id) =>
        animalDataService.Get(id) is { } animal
            ? ToResponse(animal)
            : null;

    private static AnimalProfileResponse ToResponse(AnimalDataRecord animal) =>
        new(
            animal.Id,
            animal.Name,
            animal.Species,
            animal.Breed,
            animal.PublicDescription ?? string.Empty,
            animal.AvailableForAdoption);
}
