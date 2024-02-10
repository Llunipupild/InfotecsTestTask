namespace TestTask.Tools.Range
{
    /// <summary>
    /// Класс-обёртка для промежутка среднего времени проведения эксперемента
    /// </summary>
    public class AverageExpirementTimeRange
    {
        /// <summary>
        /// Начальное среднее значение времени эсперимента
        /// </summary>
        public int AverageExpirementTimeFirst { get; set; } = int.MinValue;

        /// <summary>
        /// Конечное среднее значение времени эсперимента
        /// </summary>
        public int AverageExpirementTimeLast { get; set; } = int.MinValue;

        /// <summary>
        /// Метод, который проверяет был ли передан промежуток среднего времени проведения эксперемента
        /// </summary>
        /// <returns>Истина, если никаких значений не передавали, в ином же случае ложь</returns>
        public bool IsNull()
        {
            return AverageExpirementTimeFirst == int.MinValue && AverageExpirementTimeLast == int.MinValue;
        }
    }
}
