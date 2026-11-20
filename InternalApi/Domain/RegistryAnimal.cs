using InternalApi.Models;

namespace InternalApi.Domain;

internal sealed record RegistryAnimal(
    Guid Id,
    string RegisteredName,
    string Species,
    AnimalCategory Category,
    DateOnly? BirthDate,
    string? Breed,
    IReadOnlyDictionary<string, string>? Attributes,
    string? MedicalNotes,
    DateTimeOffset RegisteredAt,
    DateTimeOffset LastUpdatedAt);
