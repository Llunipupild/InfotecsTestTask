using System.Globalization;
using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;

namespace TestTask.Tools.Parser
{
    public static class StringParser
    {
        private const string BASE_DATETIME_PATTERN = "yyyy-MM-dd_HH-mm-ss";
        public static bool ParseDateTimeFromString(string str, out DateTime result, string pattern = BASE_DATETIME_PATTERN)
        {
            return DateTime.TryParseExact(str, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public static string ParseDateTimeToString(DateTime dateTime, string pattern = BASE_DATETIME_PATTERN)
        {
            return dateTime.ToString(pattern);
        }

        public static bool ParseIntFromString(string str, out int result)
        {
            return int.TryParse(str, out result);
        }

        public static bool ParseDoubleFromString(string str, out double result, NumberFormatInfo? numberFormatInfo = null)
        {
            numberFormatInfo ??= new() { NumberDecimalSeparator = "," };
            return double.TryParse(str, numberFormatInfo, out result);
        }

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
