using TestTask.Models.Files;
using TestTask.Repository.Base;

namespace TestTask.Repository.Files
{
    /// <summary>
    /// Интерфейс для класс-репозитория FilesRepository
    /// </summary>
    public interface IFilesRepository : IBaseRepository
    {
        bool AddOrUpdate(IFileModel fileModel);
        FileModel? GetFileModelByName(string name);
    }
}
