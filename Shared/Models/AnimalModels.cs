namespace Shared.Models;

/// <summary>Represents an animal that can be managed through the APIs.</summary>
public sealed record AnimalResponse(
    Guid Id,
    string Name,
    string Species,
    AnimalType Type,
    string? Breed,
    DateOnly? BirthDate,
    IReadOnlyDictionary<string, string>? Attributes);

/// <summary>Payload for creating an animal.</summary>
public sealed record CreateAnimalRequest
{
    public required string Name { get; init; }
    public required string Species { get; init; }
    public AnimalType Type { get; init; }
    public string? Breed { get; init; }
    public DateOnly? BirthDate { get; init; }
    public Dictionary<string, string>? Attributes { get; init; }
}

public enum AnimalType
{
    Mammal,
    Bird,
    Reptile,
    Fish
}
