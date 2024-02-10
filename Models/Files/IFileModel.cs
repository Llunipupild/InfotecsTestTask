using TestTask.Models.Base;

namespace TestTask.Models.Files
{
    /// <summary>
    /// Интерфейс для моделей из таблицы Files
    /// </summary>
    public interface IFileModel : IModel
    {
        public List<string> Data { get; set; }
    }
}
