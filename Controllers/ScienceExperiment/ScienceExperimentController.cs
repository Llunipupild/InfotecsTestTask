using InfotecsTestTask.Tools.ExpirementDataCalculator.Model;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models.Files;
using TestTask.Models.Results;
using TestTask.Models.Values;
using TestTask.Repository.Files;
using TestTask.Repository.Results;
using TestTask.Repository.Values;
using TestTask.Tools.ExpirementDataCalculator;
using TestTask.Tools.FileController;
using TestTask.Tools.Parser;
using TestTask.Tools.Range;
using TestTask.Tools.Validator;

namespace TestTask.Controllers.ScienceExperiment
{
    [Route("science")]
    [ApiController]
    public class ScienceExperimentController : ControllerBase
    {
        private const string FILE_EXTENSION = "csv";

        private readonly IFilesRepository _filesRepository;
        private readonly IValuesRepository _valuesRepository;
        private readonly IResultsRepository _resultsRepository;

        public ScienceExperimentController(IFilesRepository filesRepository, IValuesRepository valuesRepository, IResultsRepository resultsRepository) 
        {
            _filesRepository = filesRepository;
            _resultsRepository = resultsRepository;
            _valuesRepository = valuesRepository;
        }

        [HttpPost("files/")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (!file.ContentType.Contains(FILE_EXTENSION)) {
                return BadRequest($"The file must have the extension {FILE_EXTENSION}");
            }

            FileModel fileModel = await FileController.GetFileModelAsync(file);
            fileModel.Data = ScienceFileValidator.Validate(fileModel.Data);
            if (fileModel.Data.Count <= 0) {
                return BadRequest($"Experimental data have not been validated");
            }

            _filesRepository.AddOrUpdate(fileModel);

            List<ValueModel> valueModels = FileController.GetValueModelsFromFileModel(fileModel);
            _valuesRepository.AddOrUpdateRangeValueModels(valueModels);

            ResultModel resultModel = ExpirementDataCalculator.CalculateAndGetResultModel(valueModels);
            _resultsRepository.AddOrUpdate(resultModel);

            return Ok("Data saved successfully");
        }

        [HttpGet("results/")]
        public IActionResult GetResultModels(string? fileName, [FromQuery] AverageExpirementValueRange averageExpirementValueRange, [FromQuery] AverageExpirementTimeRange averageExpirementTimeRange)
        {
            if (!string.IsNullOrEmpty(fileName)) {
                ResultModel? resultModel = _resultsRepository.GetResultModelByFileName(fileName);
                return resultModel == null ? new JsonResult($"file {fileName} not found") : 
                                             new JsonResult(StringParser.ParseExpirementDataModelFromString(resultModel.Data));
            } else if (!averageExpirementValueRange.IsNull()) {
                List<ResultModel> resultModels = _resultsRepository.GetResultModelWithAverageValueOnRange(averageExpirementValueRange.AverageExpirementValueFirst, averageExpirementValueRange.AverageExpirementValueLast);
                List<ExpirementDataModel> expirementDataModels = new();
                resultModels.ForEach(rm => expirementDataModels.Add(StringParser.ParseExpirementDataModelFromString(rm.Data)));
                return new JsonResult(expirementDataModels);
            } else if (!averageExpirementTimeRange.IsNull()) {
                List<ResultModel> resultModels = _resultsRepository.GetResultModelWithAverageTimeOnRange(averageExpirementTimeRange.AverageExpirementTimeFirst, averageExpirementTimeRange.AverageExpirementTimeLast);
                List<ExpirementDataModel> expirementDataModels = new();
                resultModels.ForEach(rm => expirementDataModels.Add(StringParser.ParseExpirementDataModelFromString(rm.Data)));
                return new JsonResult(expirementDataModels);
            }

            return BadRequest("Unknown parameters");
        }

        [HttpGet("values/{fileName}")]
        public IActionResult GetValueModels(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) {
                return BadRequest("The file must not be empty");
            }

            return new JsonResult(_valuesRepository.GetValueModelsByFileName(fileName));
        }
    }
}
