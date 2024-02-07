using TestTask.Database.Context;
using TestTask.Models.Files;
using IModel = TestTask.Models.Base.IModel;

namespace TestTask.Repository.Files
{
    public class FilesRepository : IFilesRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FilesRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public bool Add<T>(T obj) where T : IModel
        {
            _databaseContext.Add(obj);
            return Save();
        }

        public bool Update<T>(T obj) where T : IModel
        {
            _databaseContext.Update(obj);
            return Save();
        }

        public bool AddOrUpdate(IFileModel fileModel)
        {
            FileModel? existedFileModel = GetFileModelByName(fileModel.FileName);
            if (existedFileModel == null) {
                Add(fileModel);
            } else {
                existedFileModel.Data = fileModel.Data;
                Update(existedFileModel);
            }

            return Save();
        }

        public bool Delete<T>(T obj) where T : IModel
        {
            _databaseContext.Remove(obj);
            return Save();
        }

        public FileModel? GetFileModelByName(string name)
        {
            return _databaseContext.Files.FirstOrDefault(f => f.FileName == name);
        }

        public bool Save()
        {
            return _databaseContext.SaveChanges() > 0;
        }
    }
}
