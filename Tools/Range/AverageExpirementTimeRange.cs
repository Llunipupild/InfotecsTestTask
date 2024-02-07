namespace TestTask.Tools.Range
{
    public class AverageExpirementTimeRange
    {
        public int AverageExpirementTimeFirst { get; set; } = int.MinValue;
        public int AverageExpirementTimeLast { get; set; } = int.MinValue;

        public bool IsNull()
        {
            return AverageExpirementTimeFirst == int.MinValue && AverageExpirementTimeLast == int.MinValue;
        }
    }
}
