using System.Collections.Concurrent;
using ExternalApi.Domain;
using ExternalApi.Models;

namespace ExternalApi.Services;

internal sealed class ExternalAnimalService : IExternalAnimalService
{
    private readonly ConcurrentDictionary<Guid, PartnerAnimalListing> _listings = new();

    public IReadOnlyCollection<AnimalProfileResponse> List() =>
        _listings.Values
            .OrderBy(listing => listing.DisplayName)
            .Select(ToResponse)
            .ToArray();

    public AnimalProfileResponse? Get(Guid id) =>
        _listings.TryGetValue(id, out var listing) ? ToResponse(listing) : null;

    private static AnimalProfileResponse ToResponse(PartnerAnimalListing listing) =>
        new(
            listing.Id,
            listing.DisplayName,
            listing.SpeciesName,
            listing.Breed,
            listing.PublicDescription,
            listing.Status == AdoptionStatus.Available);
}
