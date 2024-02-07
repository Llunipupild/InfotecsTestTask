using TestTask.Models.Results;
using TestTask.Models.Values;
using TestTask.Tools.Parser;

namespace TestTask.Tools.ExpirementDataCalculator
{
    public class ExpirementDataCalculator
    {
        public const string SEPARATOR = ";";

        public static ResultModel CalculateAndGetResultModel(List<ValueModel> valueModels)
        {
            DateTime minDate = DateTime.MaxValue, maxDate = DateTime.MinValue;
            int minExpirementTime = int.MaxValue, maxExpirementTime = int.MinValue;
            double minExpirementValue = double.MaxValue, maxExpirementValue = double.MinValue;
            double averageExpirementTime = 0, averageExpirementValue = 0;
            List<double> sortedExpirementValues = valueModels.Select(vm => vm.Value).ToList();
            sortedExpirementValues.Sort();

            for (int i = 0; i < valueModels.Count; i++) {
                ValueModel currentValueModel = valueModels[i];

                if (currentValueModel.DateTime < minDate) {
                    minDate = currentValueModel.DateTime;
                }
                if (currentValueModel.DateTime > maxDate) {
                    maxDate = currentValueModel.DateTime;
                }

                if (minExpirementTime > currentValueModel.Time) {
                    minExpirementTime = currentValueModel.Time;
                }
                if (maxExpirementTime < currentValueModel.Time) {
                    maxExpirementTime = currentValueModel.Time;
                }

                if (minExpirementValue > currentValueModel.Value) {
                    minExpirementValue = currentValueModel.Value;
                }
                if (maxExpirementValue < currentValueModel.Value) {
                    maxExpirementValue = currentValueModel.Value;
                }

                averageExpirementTime += currentValueModel.Time;
                averageExpirementValue += currentValueModel.Value;
            }

            averageExpirementTime /= valueModels.Count;
            averageExpirementValue /= valueModels.Count;

            string minDateString = StringParser.ParseDateTimeToString(minDate);
            string maxDateString = StringParser.ParseDateTimeToString(maxDate);

            double median = 0;
            if (sortedExpirementValues.Count % 2 == 0) {
                median = sortedExpirementValues.Count == 2 ? (sortedExpirementValues[0] + sortedExpirementValues[1]) :
                    (sortedExpirementValues[sortedExpirementValues.Count / 2] + sortedExpirementValues[sortedExpirementValues.Count / 2 + 1]);
                median /= 2;
            } else {
                median = sortedExpirementValues[sortedExpirementValues.Count / 2];
            }

            string result = minDateString + SEPARATOR + maxDateString + SEPARATOR + maxExpirementTime + SEPARATOR + minExpirementTime + SEPARATOR
            + averageExpirementTime + SEPARATOR + averageExpirementValue + SEPARATOR + median + SEPARATOR + maxExpirementValue + SEPARATOR
            + minExpirementValue + SEPARATOR + valueModels.Count;

            return new ResultModel() {
                FileName = valueModels[0].FileName,
                Data = result,
            };
        }
    }
}
