using TestTask.Models.Results;
using TestTask.Repository.Base;

namespace TestTask.Repository.Results
{
    public interface IResultsRepository : IBaseRepository
    {
        bool AddOrUpdate(IResultModel fileModel);
        ResultModel? GetResultModelByFileName(string fileName);
        List<ResultModel> GetResultModelWithAverageValueOnRange(double firstBorder, double lastBorder);
        List<ResultModel> GetResultModelWithAverageTimeOnRange(double firstBorder, double lastBorder);
    }
}
