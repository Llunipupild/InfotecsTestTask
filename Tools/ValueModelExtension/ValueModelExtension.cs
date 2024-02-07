using TestTask.Models.Values;

namespace InfotecsTestTask.Tools.ValueModelExtension
{
    public static class ValueModelExtension
    {
        public static bool Equal(this ValueModel valueModel, ValueModel otherValueModel)
        {
            return valueModel.Value == otherValueModel.Value && valueModel.DateTime == otherValueModel.DateTime
                && valueModel.Time == otherValueModel.Time && valueModel.FileName == otherValueModel.FileName;
        }
    }
}
