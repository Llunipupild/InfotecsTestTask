using TestTask.Tools.Parser;
using TestTask.Tools.TimeExtension;

namespace TestTask.Tools.Validator
{
    public class ScienceFileValidator
    {
        private static DateTime _startDateTime = new(2000, 1, 1, 0, 0, 0);

        public static List<string> Validate(List<string> strings)
        {
            List<string> result = new();
            if (strings.Count < 1 || strings.Count > 10000) {
                return new List<string>();
            }

            foreach (string str in strings) {
                if (!Validate(str)) {
                    continue;
                }

                result.Add(str);
            }

            return result;
        }

        public static bool Validate(string str)
        {
            string[] splittedStrings = str.Split(";");
            if (splittedStrings.Length != 3) {
                return false;
            }

            if (!StringParser.ParseDateTimeFromString(splittedStrings[0], out DateTime expirementDate)) {
                return false;
            }
            if (!expirementDate.IsInRange(_startDateTime, DateTime.Now)) {
                return false;
            }

            if (!StringParser.ParseIntFromString(splittedStrings[1], out int expirementTime)) {
                return false;
            }
            if (expirementTime <= 0) {
                return false;
            }

            if (!StringParser.ParseDoubleFromString(splittedStrings[2], out double expirementValue)) {
                return false;
            }
            if (expirementValue <= 0) {
                return false;
            }

            return true;
        }
    }
}
