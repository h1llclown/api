using Shared.Models;

namespace Shared.Stores;

public interface IAnimalStore
{
    IReadOnlyCollection<AnimalResponse> List();
    AnimalResponse? Get(Guid id);
    AnimalResponse Create(CreateAnimalRequest request);
    bool Delete(Guid id);
}
