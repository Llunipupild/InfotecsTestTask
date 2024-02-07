namespace TestTask.Tools.Range
{
    public class AverageExpirementValueRange
    {
        public double AverageExpirementValueFirst { get; set; } = double.MinValue;
        public double AverageExpirementValueLast { get; set; } = double.MinValue;

        public bool IsNull()
        {
            return AverageExpirementValueFirst == double.MinValue && AverageExpirementValueLast == double.MinValue;
        }
    }
}
