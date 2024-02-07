using TestTask.Models.Values;
using TestTask.Repository.Base;

namespace TestTask.Repository.Values
{
    public interface IValuesRepository : IBaseRepository
    {
        bool AddOrUpdateRangeValueModels(List<ValueModel> valueModels);
        List<ValueModel> GetValueModelsByFileName(string fileName);
    }
}
