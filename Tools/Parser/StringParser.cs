using System.Globalization;
using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;

namespace TestTask.Tools.Parser
{
    /// <summary>
    /// Класс-парсер, который приводит один тип к другому
    /// </summary>
    public static class StringParser
    {
        private const string BASE_DATETIME_PATTERN = "yyyy-MM-dd_HH-mm-ss";

        /// <summary>
        /// Метод, который приводит строку к DateTime
        /// </summary>
        /// <param name="str">Строка для конвертации</param>
        /// <param name="result">Конечный результат</param>
        /// <param name="pattern">Шаблон для привидения строки к DateTime</param>
        /// <returns>Истина, если преобразование успешно, в ином же случае ложь</returns>
        public static bool ParseDateTimeFromString(string str, out DateTime result, string pattern = BASE_DATETIME_PATTERN)
        {
            return DateTime.TryParseExact(str, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        /// <summary>
        /// Метод, который приводит DateTime к строке
        /// </summary>
        /// <param name="dateTime">DateTime для конвертации</param>
        /// <param name="pattern">Шаблон для приведения к строке</param>
        /// <returns>Строка</returns>
        public static string ParseDateTimeToString(DateTime dateTime, string pattern = BASE_DATETIME_PATTERN)
        {
            return dateTime.ToString(pattern);
        }

        /// <summary>
        /// Метод, который приводит строку к целому числу
        /// </summary>
        /// <param name="str">Строка для конвертации</param>
        /// <param name="result">Целое число</param>
        /// <returns>Истина, если преобразование успешно, в ином же случае ложь</returns>
        public static bool ParseIntFromString(string str, out int result)
        {
            return int.TryParse(str, out result);
        }

        /// <summary>
        /// Метод, который приводит строку к вещественному числу
        /// </summary>
        /// <param name="str">Строка для конвертации</param>
        /// <param name="result">вещественное число</param>
        /// <param name="numberFormatInfo">Параметры перевода строки</param>
        /// <returns>Истина, если преобразование успешно, в ином же случае ложь</returns>
        public static bool ParseDoubleFromString(string str, out double result, NumberFormatInfo? numberFormatInfo = null)
        {
            numberFormatInfo ??= new() { NumberDecimalSeparator = "," };
            return double.TryParse(str, numberFormatInfo, out result);
        }

        /// <summary>
        /// Метод, который приводит строку к модели данных эспиремента
        /// </summary>
        /// <param name="str">Строка для конвертации</param>
        /// <returns>Модель данных эспиремента, или же null</returns>
        public static ExpirementDataModel? ParseExpirementDataModelFromString(string str)
        {
            string[] data = str.Split(ExpirementDataCalculator.ExpirementDataCalculator.SEPARATOR);
            if (data.Length != 10) {
                return null;
            }

            return new() {
                MinDateString = data[0],
                MaxDateString = data[1],
                MaxExpirementTime = data[2],
                MinExpirementTime = data[3],
                AverageExpirementTime = data[4],
                AverageExpirementValue = data[5],
                Median = data[6],
                MaxExpirementValue = data[7],
                MinExpirementValue = data[8],
                ExpirementsCount = data[9],
            };
        }
    }
}
