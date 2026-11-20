using InternalApi.Models;

namespace InternalApi.Services;

internal interface IInternalAnimalService
{
    IReadOnlyCollection<AnimalRecordResponse> List();
    AnimalRecordResponse? Get(Guid id);
    AnimalRecordResponse Create(RegisterAnimalRequest request);
    bool Delete(Guid id);
}
