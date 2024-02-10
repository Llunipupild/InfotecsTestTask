using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;
using TestTask.Database.Context;
using TestTask.Models.Base;
using TestTask.Models.Results;
using TestTask.Tools.Parser;

namespace TestTask.Repository.Results
{
    /// <summary>
    /// Класс, отвечающий за работу с таблицой из базы данных Results
    /// </summary>
    public class ResultsRepository : IResultsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ResultsRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        /// <summary>
        /// Метод, который добавляет запись в базу данных
        /// </summary>
        /// <typeparam name="T">Базовый интерфейс для записей в базе данных</typeparam>
        /// <param name="obj">Модель IResultModel</param>
        /// <returns>Истина, если запись была добавлена, в мном же случае ложь</returns>
        public bool Add<T>(T obj) where T : IModel
        {
            _databaseContext.Add(obj!);
            return Save();
        }

        /// <summary>
        /// Метод, который обновляет запись в базе данных
        /// </summary>
        /// <typeparam name="T">Базовый интерфейс для записей в базе данных</typeparam>
        /// <param name="obj">Модель IResultModel</param>
        /// <returns>Истина, если запись была обновлена, в мном же случае ложь</returns>
        public bool Update<T>(T obj) where T : IModel
        {
            _databaseContext.Update(obj!);
            return Save();
        }

        /// <summary>
        /// Метод, который при отсутвии модели в базе данных её добавляет, в ином же случаи иззменяет
        /// </summary>
        /// <param name="resultModel">Модель IResultModel</param>
        /// <returns>Истина, если операция была успешна, в мном же случае ложь</returns>
        public bool AddOrUpdate(IResultModel resultModel)
        {
            ResultModel? existedResultModel = GetResultModelByFileName(resultModel.FileName);
            if (existedResultModel == null) {
                Add(resultModel);
            } else {
                existedResultModel.Data = resultModel.Data;
                Update(existedResultModel);
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
            _databaseContext.Remove(obj!);
            return Save();
        }

        /// <summary>
        /// Метод, который возвращает ResultModel из бд по имени файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Модель ResultModel или null</returns>
        public ResultModel? GetResultModelByFileName(string fileName)
        {
            return _databaseContext.Results.FirstOrDefault(r => r.FileName == fileName);
        }

        /// <summary>
        /// Метод, который возвращает список ResultModel из базы данных, у которых среднее значение эксперемента находится в промежутке
        /// </summary>
        /// <param name="firstBorder">Начало промежутка</param>
        /// <param name="lastBorder">Конец промежутка</param>
        /// <returns>Список моделей ResultModel</returns>
        public List<ResultModel> GetResultModelWithAverageValueOnRange(double firstBorder, double lastBorder)
        {
            List<ResultModel> result = new();
            foreach (ResultModel resultModel in _databaseContext.Results) {
                ExpirementDataModel expirementDataModel = StringParser.ParseExpirementDataModelFromString(resultModel.Data)!;
                double averageExpirementValue = double.Parse(expirementDataModel.AverageExpirementValue);
                if (averageExpirementValue > firstBorder && averageExpirementValue < lastBorder) {
                    result.Add(resultModel);
                }
            }
            return result;
        }

        /// <summary>
        /// Метод, который возвращает список ResultModel из базы данных, у которых среднее значение времени эксперемента находится в промежутке
        /// </summary>
        /// <param name="firstBorder">Начало промежутка</param>
        /// <param name="lastBorder">Конец промежутка</param>
        /// <returns>Список моделей ResultModel</returns>
        public List<ResultModel> GetResultModelWithAverageTimeOnRange(double firstBorder, double lastBorder)
        {
            List<ResultModel> result = new();
            foreach (ResultModel resultModel in _databaseContext.Results) {
                ExpirementDataModel expirementDataModel = StringParser.ParseExpirementDataModelFromString(resultModel.Data)!;
                double averageExpirementTime = double.Parse(expirementDataModel.AverageExpirementTime);
                if (averageExpirementTime > firstBorder && averageExpirementTime < lastBorder) {
                    result.Add(resultModel);
                }
            }
            return result;
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
