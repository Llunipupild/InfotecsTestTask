namespace TestTask.Tools.Range
{
    /// <summary>
    /// Класс-обёртка для промежутка среднего значения эксперемента
    /// </summary>
    public class AverageExpirementValueRange
    {
        /// <summary>
        /// Начальное среднее значение эсперимента
        /// </summary>
        public double AverageExpirementValueFirst { get; set; } = double.MinValue;

        /// <summary>
        /// Конечное среднее значение эсперимента
        /// </summary>
        public double AverageExpirementValueLast { get; set; } = double.MinValue;

        /// <summary>
        /// Метод, который проверяет был ли передан промежуток среднего значения эксперемента
        /// </summary>
        /// <returns>Истина, если никаких значений не передавали, в ином же случае ложь</returns>
        public bool IsNull()
        {
            return AverageExpirementValueFirst == double.MinValue && AverageExpirementValueLast == double.MinValue;
        }
    }
}
