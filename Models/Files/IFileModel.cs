using TestTask.Models.Base;

namespace TestTask.Models.Files
{
    public interface IFileModel : IModel
    {
        public List<string> Data { get; set; }
    }
}
