using ExternalApi.Models;

namespace ExternalApi.Services;

internal interface IExternalAnimalService
{
    IReadOnlyCollection<AnimalProfileResponse> List();
    AnimalProfileResponse? Get(Guid id);
}
