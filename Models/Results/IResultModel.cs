using TestTask.Models.Base;

namespace TestTask.Models.Results
{
    /// <summary>
    /// Интерфейс для моделей из таблицы Results
    /// </summary>
    public interface IResultModel : IModel
    {
        public string Data { get; set; }
    }
}
