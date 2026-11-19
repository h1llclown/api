using Shared.Models;
using Shared.Stores;

namespace ExternalApi.Stores;

public sealed class ExternalAnimalStore : IAnimalStore
{
    private readonly Dictionary<Guid, AnimalResponse> _animals = new();

    public IReadOnlyCollection<AnimalResponse> List() =>
        _animals.Values.OrderBy(animal => animal.Name).ToArray();

    public AnimalResponse? Get(Guid id) =>
        _animals.TryGetValue(id, out var animal) ? animal : null;

    public AnimalResponse Create(CreateAnimalRequest request)
    {
        var animal = new AnimalResponse(
            Guid.NewGuid(), request.Name, request.Species, request.Type,
            request.Breed, request.BirthDate, request.Attributes);
        _animals[animal.Id] = animal;
        return animal;
    }

    public bool Delete(Guid id) => _animals.Remove(id);
}
