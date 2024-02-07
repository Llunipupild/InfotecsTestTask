using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;

namespace InfotecsTestTask.Tools.ExpirementDataCalculator.Extension
{
    public static class ExpirementDataModelExtension
    {
        public static bool Equal(this ExpirementDataModel expirementDataModel, ExpirementDataModel otherModel)
        {
            return expirementDataModel.MinExpirementValue == otherModel.MinExpirementValue && expirementDataModel.MaxExpirementValue == otherModel.MaxExpirementValue && expirementDataModel.AverageExpirementValue == otherModel.AverageExpirementValue &&
               expirementDataModel.Median == otherModel.Median && expirementDataModel.AverageExpirementTime == otherModel.AverageExpirementTime && expirementDataModel.MinDateString == otherModel.MinDateString && expirementDataModel.MaxDateString == otherModel.MaxDateString;
        }
    }
}
