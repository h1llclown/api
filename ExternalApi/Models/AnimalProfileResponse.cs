namespace ExternalApi.Models;

/// <summary>Public animal-profile data returned by the external API.</summary>
public sealed record AnimalProfileResponse(
    Guid Id,
    string Name,
    string Species,
    string? Breed,
    string Description,
    bool AvailableForAdoption);
