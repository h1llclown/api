using System.Collections.Concurrent;
using InternalApi.Domain;
using InternalApi.Models;

namespace InternalApi.Services;

internal sealed class InternalAnimalService : IInternalAnimalService
{
    private readonly ConcurrentDictionary<Guid, RegistryAnimal> _animals = new();

    public IReadOnlyCollection<AnimalRecordResponse> List() =>
        _animals.Values
            .OrderBy(animal => animal.RegisteredName)
            .Select(ToResponse)
            .ToArray();

    public AnimalRecordResponse? Get(Guid id) =>
        _animals.TryGetValue(id, out var animal) ? ToResponse(animal) : null;

    public AnimalRecordResponse Create(RegisterAnimalRequest request)
    {
        var registeredAt = DateTimeOffset.UtcNow;
        var animal = new RegistryAnimal(
            Guid.NewGuid(),
            request.RegisteredName,
            request.Species,
            request.Category,
            request.BirthDate,
            request.Breed,
            request.Attributes is null
                ? null
                : new Dictionary<string, string>(request.Attributes),
            request.MedicalNotes,
            registeredAt,
            registeredAt);

        _animals[animal.Id] = animal;
        return ToResponse(animal);
    }

    public bool Delete(Guid id) => _animals.TryRemove(id, out _);

    private static AnimalRecordResponse ToResponse(RegistryAnimal animal) =>
        new(
            animal.Id,
            animal.RegisteredName,
            animal.Species,
            animal.Category,
            animal.BirthDate,
            animal.Breed,
            animal.Attributes,
            animal.MedicalNotes,
            animal.RegisteredAt,
            animal.LastUpdatedAt);
}
