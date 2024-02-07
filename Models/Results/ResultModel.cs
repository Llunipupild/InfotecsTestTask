namespace TestTask.Models.Results
{
    public class ResultModel : IResultModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string Data { get; set; } = null!;
    }
}
