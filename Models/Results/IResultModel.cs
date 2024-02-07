using TestTask.Models.Base;

namespace TestTask.Models.Results
{
    public interface IResultModel : IModel
    {
        public string Data { get; set; }
    }
}
