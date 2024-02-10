using TestTask.Database.Context;
using TestTask.Models.Base;
using TestTask.Models.Values;

namespace TestTask.Repository.Values
{
    /// <summary>
    /// Класс, отвечающий за работу с таблицой из базы данных Values
    /// </summary>
    public class ValuesRepository : IValuesRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ValuesRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        /// <summary>
        /// Метод, который добавляет запись в базу данных
        /// </summary>
        /// <typeparam name="T">Базовый интерфейс для записей в базе данных</typeparam>
        /// <param name="obj">Модель IValuesModel</param>
        /// <returns>Истина, если запись была добавлена, в мном же случае ложь</returns>
        public bool Add<T>(T obj) where T : IModel
        {
            _databaseContext.Add(obj);
            return Save();
        }

        /// <summary>
        /// Метод, который обновляет запись в базе данных
        /// </summary>
        /// <typeparam name="T">Базовый интерфейс для записей в базе данных</typeparam>
        /// <param name="obj">Модель IValuesModel</param>
        /// <returns>Истина, если запись была обновлена, в мном же случае ложь</returns>
        public bool Update<T>(T obj) where T : IModel
        {
            _databaseContext.Update(obj);
            return Save();
        }

        /// <summary>
        /// Метод, который удляет старые данные в базе данных и добавляет новые
        /// </summary>
        /// <param name="valueModels"> Список ValueModel</param>
        /// <returns>Истина, если операция была выполнена успешно, в мном же случае ложь</returns>
        public bool AddOrUpdateRangeValueModels(List<ValueModel> valueModels)
        {
            List<ValueModel> existedValueModels = GetValueModelsByFileName(valueModels.First().FileName);
            existedValueModels.ForEach(evm => Delete((IModel) evm));
            valueModels.ForEach(vm => Add((IModel) vm));
            return Save();
        }

        /// <summary>
        /// Метод, который удаляет запись в базе данных
        /// </summary>
        /// <typeparam name="T">Базовый интерфейс для записей в базе данных</typeparam>
        /// <param name="obj">Модель IValuesModel</param>
        /// <returns>Истина, если запись была удалена, в мном же случае ложь</returns>
        public bool Delete<T>(T obj) where T : IModel
        {
            _databaseContext.Remove(obj);
            return Save();
        }

        /// <summary>
        /// Метод, который возвращает список ValueModel из бд по имени файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Модель ValueModel или null</returns>
        public List<ValueModel> GetValueModelsByFileName(string fileName)
        {
            return _databaseContext.Values.Where(vm => vm.FileName == fileName).ToList();
        }

        /// <summary>
        /// Метод, который сохраняет изменения в базе данных
        /// </summary>
        /// <returns>Истина если измененмя сохранены в ином же случае ложь</returns>
        public bool Save()
        {
            return _databaseContext.SaveChanges() > 0;
        }
    }
}
