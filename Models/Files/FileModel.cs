namespace TestTask.Models.Files
{
    /// <summary>
    /// Класс-модель, содержащий данные о записи из таблицы Files
    /// </summary>
    public class FileModel : IFileModel
    {
        public int Id { get; set; }
        public string Author { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public List<string> Data { get; set; } = null!;
    }
}
