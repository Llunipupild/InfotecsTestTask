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
    /// <summary>
    /// Класс-контроллер для работы с данными научного эксперимента
    /// </summary>
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

        /// <summary>
        /// Метод http post к точке /science/files/
        /// </summary>
        /// <param name="file">Файл с данными об эксперименте</param>
        /// <returns>Записанные данные в базу данных эксперимента</returns>
        /// <response code="201">Запись в базе данных сделана</response>
        /// <response code="400">Файл не подходит</response>
        [HttpPost("files/")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(ResultModel))]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (!file.ContentType.Contains(FILE_EXTENSION)) {
                return BadRequest($"Файл дожен иметь расширение {FILE_EXTENSION}");
            }

            FileModel fileModel = await FileController.GetFileModelAsync(file);
            fileModel.Data = ScienceFileValidator.Validate(fileModel.Data);
            if (fileModel.Data.Count <= 0) {
                return BadRequest($"Данные не прошли валидацию");
            }

            _filesRepository.AddOrUpdate(fileModel);

            List<ValueModel> valueModels = FileController.GetValueModelsFromFileModel(fileModel);
            _valuesRepository.AddOrUpdateRangeValueModels(valueModels);

            ResultModel resultModel = ExpirementDataCalculator.CalculateAndGetResultModel(valueModels);
            _resultsRepository.AddOrUpdate(resultModel);

            return StatusCode(StatusCodes.Status201Created, resultModel);
        }

        /// <summary>
        /// Метод http get к конечной точке /science/results/
        /// </summary>
        /// <param name="fileName">Фильтр по имени файла</param>
        /// <param name="averageExpirementValueRange">Фильтр по промежутку среднего значения эксперимента</param>
        /// <param name="averageExpirementTimeRange">Фильтр по промежутку среднего времени эксперимента</param>
        /// <returns>Вычисленные данные о научных экспериментах</returns>
        /// <response code="200">Данные найдены</response>
        /// <response code="400">Неизвестные параметры</response>
        [HttpGet("results/")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ExpirementDataModel))]
        public IActionResult GetResultModels(string? fileName, [FromQuery] AverageExpirementValueRange averageExpirementValueRange, [FromQuery] AverageExpirementTimeRange averageExpirementTimeRange)
        {
            if (!string.IsNullOrEmpty(fileName)) {
                ResultModel? resultModel = _resultsRepository.GetResultModelByFileName(fileName);
                return resultModel == null ? Ok($"Файл {fileName} не найден") : 
                                             Ok(StringParser.ParseExpirementDataModelFromString(resultModel.Data));
            } else if (!averageExpirementValueRange.IsNull()) {
                List<ResultModel> resultModels = _resultsRepository.GetResultModelWithAverageValueOnRange(averageExpirementValueRange.AverageExpirementValueFirst, averageExpirementValueRange.AverageExpirementValueLast);
                List<ExpirementDataModel> expirementDataModels = new();
                resultModels.ForEach(rm => expirementDataModels.Add(StringParser.ParseExpirementDataModelFromString(rm.Data)!));
                return Ok(expirementDataModels);
            } else if (!averageExpirementTimeRange.IsNull()) {
                List<ResultModel> resultModels = _resultsRepository.GetResultModelWithAverageTimeOnRange(averageExpirementTimeRange.AverageExpirementTimeFirst, averageExpirementTimeRange.AverageExpirementTimeLast);
                List<ExpirementDataModel> expirementDataModels = new();
                resultModels.ForEach(rm => expirementDataModels.Add(StringParser.ParseExpirementDataModelFromString(rm.Data)!));
                return Ok(expirementDataModels);
            }

            return BadRequest("Неизвестные параметры");
        }

        /// <summary>
        /// Метод http get к точке /science/values/{fileName}
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Записи из таблицы Values </returns>
        [HttpGet("values/{fileName}")]
        [ProducesResponseType(200, Type = typeof(ResultModel))]
        [ProducesResponseType(400)]
        public IActionResult GetValueModels(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) {
                return BadRequest("Файл не может быть пустым");
            }

            return new JsonResult(_valuesRepository.GetValueModelsByFileName(fileName));
        }
    }
}
