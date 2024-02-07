using InfotecsTestTask.Tools.ExpirementDataCalculator.Extension;
using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;
using InfotecsTestTask.Tools.ValueModelExtension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Controllers.ScienceExperiment;
using TestTask.Database.Context;
using TestTask.Models.Files;
using TestTask.Models.Results;
using TestTask.Models.Values;
using TestTask.Repository.Files;
using TestTask.Repository.Results;
using TestTask.Repository.Values;
using TestTask.Tools.ExpirementDataCalculator;
using TestTask.Tools.FileController;
using TestTask.Tools.Parser;
using TestTask.Tools.Validator;

namespace XUnitTestProject
{
    public class MainTests
    {
        [Fact]
        public void TestScienceFileValidator()
        {
            Assert.False(ScienceFileValidator.Validate("2025-03-18_09-18-17;1300;900,353"));
            Assert.False(ScienceFileValidator.Validate("1999-03-18_09-18-17;1744;1632,472"));
            Assert.False(ScienceFileValidator.Validate("2022-03-18_09-18-17;0;1632,472"));
            Assert.False(ScienceFileValidator.Validate("2022-03-18_09-18-17;1000;0"));
            Assert.False(ScienceFileValidator.Validate(""));
            Assert.False(ScienceFileValidator.Validate("0;0"));

            Assert.True(ScienceFileValidator.Validate("2023-10-20_05-12-10;4444;5350"));
            Assert.True(ScienceFileValidator.Validate("2023-05-20_05-12-10;3678;150,15"));
            Assert.True(ScienceFileValidator.Validate("2021-09-18_09-18-17;2300;2000,350"));
        }

        [Fact]
        public void TestScienceExpirementController()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("Data Source=InfotecsTestTask.db")
                .Options;

            DatabaseContext databaseContext = new(options);
            IFilesRepository filesRepository = new FilesRepository(databaseContext);
            IValuesRepository valuesRepository = new ValuesRepository(databaseContext);
            IResultsRepository resultsRepository = new ResultsRepository(databaseContext);
            ScienceExperimentController controller = new(filesRepository, valuesRepository, resultsRepository);

            IFileModel fileModel = new FileModel() {
                Author = "testUser",
                FileName = "test.csv",
                CreatedDate = DateTime.Now,
                Data = new() { "2023-10-20_05-12-10;4444;5350" }
            };
            Assert.True(filesRepository.Add(fileModel));
            fileModel.Data = new() { "2023-05-20_05-12-10;3678;150,15" };

            List<ValueModel> valueModels = FileController.GetValueModelsFromFileModel((FileModel) fileModel);
            Assert.True(valuesRepository.Add(valueModels.First()));

            ResultModel resultModel = ExpirementDataCalculator.CalculateAndGetResultModel(valueModels);
            Assert.True(resultsRepository.Add(resultModel));

            IActionResult resultModelResult = controller.GetResultModels("test.csv", null!, null!);
            IActionResult valueModelsResult = controller.GetValueModels("test.csv");

            JsonResult jsonModelResult = (JsonResult) resultModelResult;
            JsonResult jsonValueResult = (JsonResult) valueModelsResult;

            ExpirementDataModel expirementDataModelResult = (ExpirementDataModel) jsonModelResult.Value!;
            List<ValueModel> valueModelResult = (List<ValueModel>) jsonValueResult.Value!;

            ExpirementDataModel expirementDataModel = new() {
                AverageExpirementTime = "3678",
                AverageExpirementValue = "150,15",
                ExpirementsCount = "1",
                MaxDateString = "2023-05-20_05-12-10",
                MaxExpirementTime = "3678",
                MaxExpirementValue = "150,15",
                Median = "150,15",
                MinDateString = "2023-05-20_05-12-10",
                MinExpirementTime = "3678",
                MinExpirementValue = "150,15"
            };

            StringParser.ParseDateTimeFromString(expirementDataModel.MinDateString, out DateTime dateTime);
            ValueModel rm = new() {
                FileName = resultModel.FileName,
                Time = 3678,
                Value = 150.15,
                DateTime = dateTime,
            };

            Assert.True(expirementDataModelResult.Equal(expirementDataModel));
            Assert.True(rm.Equal(valueModelResult.First()));

            Assert.True(filesRepository.Delete(fileModel));
            Assert.True(valuesRepository.Delete(valueModels.First()));
            Assert.True(filesRepository.Delete(resultModel));
        }
    }
}