namespace ExternalApi.Domain;

internal sealed record PartnerAnimalListing(
    Guid Id,
    string DisplayName,
    string SpeciesName,
    string? Breed,
    string PublicDescription,
    AdoptionStatus Status);

internal enum AdoptionStatus
{
    Available,
    Reserved,
    Adopted
}
