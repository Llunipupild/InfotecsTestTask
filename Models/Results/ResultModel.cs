namespace TestTask.Models.Results
{
    /// <summary>
    /// Класс-модель, содержащий данные о записи из таблицы Results
    /// </summary>
    public class ResultModel : IResultModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string Data { get; set; } = null!;
    }
}
