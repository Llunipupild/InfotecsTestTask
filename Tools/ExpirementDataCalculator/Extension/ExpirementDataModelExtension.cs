using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;

namespace InfotecsTestTask.Tools.ExpirementDataCalculator.Extension
{
    /// <summary>
    /// Extension класс для класса ExpirementDataModel
    /// </summary>
    public static class ExpirementDataModelExtension
    {
        /// <summary>
        /// Метод сравнения двух объектов ExpirementDataModel
        /// </summary>
        /// <param name="expirementDataModel">Первая модель для сравнения</param>
        /// <param name="otherModel">Вторая модель для сравнения</param>
        /// <returns>возвращает истину если объекты равны в ином же случае ложь</returns>
        public static bool Compare(this ExpirementDataModel expirementDataModel, ExpirementDataModel otherModel)
        {
            return expirementDataModel.MinExpirementValue == otherModel.MinExpirementValue && expirementDataModel.MaxExpirementValue == otherModel.MaxExpirementValue && expirementDataModel.AverageExpirementValue == otherModel.AverageExpirementValue &&
               expirementDataModel.Median == otherModel.Median && expirementDataModel.AverageExpirementTime == otherModel.AverageExpirementTime && expirementDataModel.MinDateString == otherModel.MinDateString && expirementDataModel.MaxDateString == otherModel.MaxDateString;
        }
    }
}
