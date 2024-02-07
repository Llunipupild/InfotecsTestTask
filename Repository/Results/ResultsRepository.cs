using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;
using TestTask.Database.Context;
using TestTask.Models.Base;
using TestTask.Models.Results;
using TestTask.Tools.Parser;

namespace TestTask.Repository.Results
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ResultsRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public bool Add<T>(T obj) where T : IModel
        {
            _databaseContext.Add(obj!);
            return Save();
        }

        public bool Update<T>(T obj) where T : IModel
        {
            _databaseContext.Update(obj!);
            return Save();
        }

        public bool AddOrUpdate(IResultModel resultModel)
        {
            ResultModel? existedResultModel = GetResultModelByFileName(resultModel.FileName);
            if (existedResultModel == null) {
                Add(resultModel);
            } else {
                existedResultModel.Data = resultModel.Data;
                Update(existedResultModel);
            }

            return Save();
        }

        public bool Delete<T>(T obj) where T : IModel
        {
            _databaseContext.Remove(obj!);
            return Save();
        }

        public ResultModel? GetResultModelByFileName(string fileName)
        {
            return _databaseContext.Results.FirstOrDefault(r => r.FileName == fileName);
        }

        public List<ResultModel> GetResultModelWithAverageValueOnRange(double firstBorder, double lastBorder)
        {
            List<ResultModel> result = new();
            foreach (ResultModel resultModel in _databaseContext.Results) {
                ExpirementDataModel expirementDataModel = StringParser.ParseExpirementDataModelFromString(resultModel.Data)!;
                double averageExpirementValue = double.Parse(expirementDataModel.AverageExpirementValue);
                if (averageExpirementValue > firstBorder && averageExpirementValue < lastBorder) {
                    result.Add(resultModel);
                }
            }
            return result;
        }

        public List<ResultModel> GetResultModelWithAverageTimeOnRange(double firstBorder, double lastBorder)
        {
            List<ResultModel> result = new();
            foreach (ResultModel resultModel in _databaseContext.Results) {
                ExpirementDataModel expirementDataModel = StringParser.ParseExpirementDataModelFromString(resultModel.Data)!;
                double averageExpirementTime = double.Parse(expirementDataModel.AverageExpirementTime);
                if (averageExpirementTime > firstBorder && averageExpirementTime < lastBorder) {
                    result.Add(resultModel);
                }
            }
            return result;
        }

        public bool Save()
        {
            return _databaseContext.SaveChanges() > 0;
        }
    }
}
