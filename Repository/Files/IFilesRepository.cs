using TestTask.Models.Files;
using TestTask.Repository.Base;

namespace TestTask.Repository.Files
{
    public interface IFilesRepository : IBaseRepository
    {
        bool AddOrUpdate(IFileModel fileModel);
        FileModel? GetFileModelByName(string name);
    }
}
