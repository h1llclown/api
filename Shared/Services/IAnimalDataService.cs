using Shared.Models;

namespace Shared.Services;

public interface IAnimalDataService
{
    IReadOnlyCollection<AnimalDataRecord> List();
    AnimalDataRecord? Get(Guid id);
    AnimalDataRecord Create(CreateAnimalDataRequest request);
    bool Delete(Guid id);
}
