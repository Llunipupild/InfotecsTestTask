using TestTask.Models.Values;

namespace InfotecsTestTask.Tools.ValueModelExtension
{
    /// <summary>
    /// Класс, в котором хранятся методы расширения для класса ValueModel
    /// </summary>
    public static class ValueModelExtension
    {
        /// <summary>
        /// Метод, который сравнивает значения моделей
        /// </summary>
        /// <param name="valueModel">Первая модель</param>
        /// <param name="otherValueModel">Вторая модель</param>
        /// <returns>Истину, если поля моделей равны, в ином случае ложь</returns>
        public static bool Compare(this ValueModel valueModel, ValueModel otherValueModel)
        {
            return valueModel.Value == otherValueModel.Value && valueModel.DateTime == otherValueModel.DateTime
                && valueModel.Time == otherValueModel.Time && valueModel.FileName == otherValueModel.FileName;
        }
    }
}
