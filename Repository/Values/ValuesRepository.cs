using TestTask.Database.Context;
using TestTask.Models.Base;
using TestTask.Models.Values;

namespace TestTask.Repository.Values
{
    public class ValuesRepository : IValuesRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ValuesRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public bool Add<T>(T obj) where T : IModel
        {
            _databaseContext.Add(obj);
            return Save();
        }

        public bool Update<T>(T obj) where T : IModel
        {
            _databaseContext.Update(obj);
            return Save();
        }

        public bool AddOrUpdateRangeValueModels(List<ValueModel> valueModels)
        {
            List<ValueModel> existedValueModels = GetValueModelsByFileName(valueModels.First().FileName);
            existedValueModels.ForEach(evm => Delete((IModel) evm));
            valueModels.ForEach(vm => Add((IModel) vm));
            return Save();
        }

        public bool Delete<T>(T obj) where T : IModel
        {
            _databaseContext.Remove(obj);
            return Save();
        }

        public List<ValueModel> GetValueModelsByFileName(string fileName)
        {
            return _databaseContext.Values.Where(vm => vm.FileName == fileName).ToList();
        }

        public bool Save()
        {
            return _databaseContext.SaveChanges() > 0;
        }
    }
}
