namespace TestTask.Models.Base
{
    /// <summary>
    /// Базовый интерфейс для записей в базе данных
    /// </summary>
    public interface IModel
    {
        string FileName { get; set; }
    }
}
