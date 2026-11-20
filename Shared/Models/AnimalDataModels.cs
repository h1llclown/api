using System.Text.Json.Serialization;

namespace Shared.Models;

/// <summary>Animal category used by the shared animal data service.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AnimalCategory
{
    Mammal,
    Bird,
    Reptile,
    Fish
}

/// <summary>Canonical animal data shared by the external and internal APIs.</summary>
public sealed record AnimalDataRecord(
    Guid Id,
    string Name,
    string Species,
    AnimalCategory Category,
    DateOnly? BirthDate,
    string? Breed,
    IReadOnlyDictionary<string, string>? Attributes,
    string? MedicalNotes,
    string? PublicDescription,
    bool AvailableForAdoption,
    DateTimeOffset RegisteredAt,
    DateTimeOffset LastUpdatedAt);

/// <summary>Request to add an animal to the shared data service.</summary>
public sealed record CreateAnimalDataRequest(
    string Name,
    string Species,
    AnimalCategory Category,
    DateOnly? BirthDate,
    string? Breed,
    Dictionary<string, string>? Attributes,
    string? MedicalNotes,
    string? PublicDescription,
    bool AvailableForAdoption);
