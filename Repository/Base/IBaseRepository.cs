using IModel = TestTask.Models.Base.IModel;

namespace TestTask.Repository.Base
{
    /// <summary>
    /// Базовый интерфейс для классов-репозиториев
    /// </summary>
    public interface IBaseRepository
    {
        bool Add<T>(T obj) where T : IModel;
        bool Update<T>(T obj) where T : IModel;
        bool Delete<T>(T obj) where T : IModel;
        bool Save();
    }
}
