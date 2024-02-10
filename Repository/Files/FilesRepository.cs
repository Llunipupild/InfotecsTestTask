using TestTask.Database.Context;
using TestTask.Models.Files;
using IModel = TestTask.Models.Base.IModel;

namespace TestTask.Repository.Files
{
    /// <summary>
    /// Класс, отвечающий за работу с таблицой из базы данных Files
    /// </summary>
    public class FilesRepository : IFilesRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FilesRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        /// <summary>
        /// Метод, который добавляет запись в базу данных
        /// </summary>
        /// <typeparam name="T">Базовый интерфейс для записей в базе данных</typeparam>
        /// <param name="obj">Модель IFileModel</param>
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
        /// <param name="obj">Модель IFileModel</param>
        /// <returns>Истина, если запись была обновлена, в мном же случае ложь</returns>
        public bool Update<T>(T obj) where T : IModel
        {
            _databaseContext.Update(obj);
            return Save();
        }

        /// <summary>
        /// Метод, который при отсутвии модели в базе данных её добавляет, в ином же случаи иззменяет
        /// </summary>
        /// <param name="fileModel">Модель IFileModel</param>
        /// <returns>Истина, если операция была успешна, в мном же случае ложь</returns>
        public bool AddOrUpdate(IFileModel fileModel)
        {
            FileModel? existedFileModel = GetFileModelByName(fileModel.FileName);
            if (existedFileModel == null) {
                Add(fileModel);
            } else {
                existedFileModel.Data = fileModel.Data;
                Update(existedFileModel);
            }

            return Save();
        }

        /// <summary>
        /// Метод, который удаляет запись в базе данных
        /// </summary>
        /// <typeparam name="T">Базовый интерфейс для записей в базе данных</typeparam>
        /// <param name="obj">Модель IFileModel</param>
        /// <returns>Истина, если запись была удалена, в мном же случае ложь</returns>
        public bool Delete<T>(T obj) where T : IModel
        {
            _databaseContext.Remove(obj);
            return Save();
        }

        /// <summary>
        /// Метод, который возвращает FileModel из бд по имени файла
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <returns>Модель FileModel или null</returns>
        public FileModel? GetFileModelByName(string name)
        {
            return _databaseContext.Files.FirstOrDefault(f => f.FileName == name);
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
