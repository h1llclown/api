using System.ComponentModel.DataAnnotations;
using Shared.Models;

namespace InternalApi.Models;

/// <summary>Payload for registering an animal in the internal registry.</summary>
public sealed record RegisterAnimalRequest
{
    [Required, MaxLength(200)]
    public required string RegisteredName { get; init; }

    [Required, MaxLength(100)]
    public required string Species { get; init; }

    public required AnimalCategory Category { get; init; }

    public DateOnly? BirthDate { get; init; }

    [MaxLength(100)]
    public string? Breed { get; init; }

    public Dictionary<string, string>? Attributes { get; init; }

    [MaxLength(2000)]
    public string? MedicalNotes { get; init; }

    [MaxLength(2000)]
    public string? PublicDescription { get; init; }

    public bool AvailableForAdoption { get; init; } = true;
}

/// <summary>Animal registry data returned to authorized internal clients.</summary>
public sealed record AnimalRecordResponse(
    Guid Id,
    string RegisteredName,
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
