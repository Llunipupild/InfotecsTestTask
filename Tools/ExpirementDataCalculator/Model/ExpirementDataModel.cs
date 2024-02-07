namespace InfotecsTestTask.Tools.ExpirementDataCalculator.Model
{
    public class ExpirementDataModel
    {
        public string MinDateString { get; set; } = null!;
        public string MaxDateString { get; set; } = null!;
        public string MinExpirementTime { get; set; } = null!;
        public string MaxExpirementTime { get; set; } = null!;
        public string MinExpirementValue { get; set; } = null!;
        public string MaxExpirementValue { get; set; } = null!;
        public string AverageExpirementTime { get; set; } = null!;
        public string AverageExpirementValue { get; set; } = null!;
        public string Median { get; set; } = null!;
        public string ExpirementsCount { get; set; } = null!;
    }
}
