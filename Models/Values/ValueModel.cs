namespace TestTask.Models.Values
{
    public class ValueModel : IValueModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int Time { get; set; }
        public double Value { get; set; }
    }
}
