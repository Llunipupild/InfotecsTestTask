namespace TestTask.Tools.TimeExtension
{
    /// <summary>
    /// Класс, в котором хранятся методы расширения для класса DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Метод, проверющий входит ли дата в заданный промежуток
        /// </summary>
        /// <param name="dateToCheck">Дата для проверки</param>
        /// <param name="startDate">Стартовая дата откуда начинается промежуток</param>
        /// <param name="endDate">Конечная дата где заканчивает промежуток</param>
        /// <returns>Истина, если дата входит в промежуток, в ином же случае ложь</returns>
        public static bool IsInRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }
}
