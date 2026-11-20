using System.Collections.Concurrent;
using Shared.Models;

namespace Shared.Services;

public sealed class AnimalDataService : IAnimalDataService
{
    private readonly ConcurrentDictionary<Guid, AnimalDataRecord> _animals = new();

    public IReadOnlyCollection<AnimalDataRecord> List() =>
        _animals.Values
            .OrderBy(animal => animal.Name)
            .ToArray();

    public AnimalDataRecord? Get(Guid id) =>
        _animals.TryGetValue(id, out var animal) ? animal : null;

    public AnimalDataRecord Create(CreateAnimalDataRequest request)
    {
        var registeredAt = DateTimeOffset.UtcNow;
        var animal = new AnimalDataRecord(
            Guid.NewGuid(),
            request.Name,
            request.Species,
            request.Category,
            request.BirthDate,
            request.Breed,
            request.Attributes is null
                ? null
                : new Dictionary<string, string>(request.Attributes),
            request.MedicalNotes,
            request.PublicDescription,
            request.AvailableForAdoption,
            registeredAt,
            registeredAt);

        _animals[animal.Id] = animal;
        return animal;
    }

    public bool Delete(Guid id) => _animals.TryRemove(id, out _);
}
