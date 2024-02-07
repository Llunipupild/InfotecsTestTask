using System.Security.AccessControl;
using System.Security.Principal;
using TestTask.Models.Files;
using TestTask.Models.Values;
using TestTask.Tools.Parser;

namespace TestTask.Tools.FileController
{
    public class FileController
    {
        private const string UPLOAD_FILES_DIRECTORY = "uploadfiles";

        public async static Task<FileModel> GetFileModelAsync(IFormFile file)
        {
            string filePath = await UploadFileAsync(file);
            List<string> fileData = await ReadAllLinesFromFileAsync(filePath);
            DateTime createdTime = File.GetCreationTime(filePath);
            string fileOwner = GetFileOwnerAndRemoveFile(filePath);

            return new FileModel() {
                Author = fileOwner,
                FileName = file.FileName,
                CreatedDate = createdTime,
                Data = fileData
            };
        }

        public static List<ValueModel> GetValueModelsFromFileModel(FileModel fileModel)
        {
            List<ValueModel> result = new();
            foreach (string str in fileModel.Data) {
                string[] splittedStrings = str.Split(";");

                if (!StringParser.ParseDateTimeFromString(splittedStrings[0], out DateTime expirementDate)) {
                    continue;
                }
                if (!StringParser.ParseIntFromString(splittedStrings[1], out int expirementTime)) {
                    continue;
                }
                if (!StringParser.ParseDoubleFromString(splittedStrings[2], out double expirementValue)) {
                    continue;
                }

                result.Add(new() {
                    FileName = fileModel.FileName,
                    DateTime = expirementDate,
                    Value = expirementValue,
                    Time = expirementTime
                });
            }

            return result;
        }

        public async static Task<string> UploadFileAsync(IFormFile file)
        {
            string fileDirectory = Directory.GetCurrentDirectory() + "\\" + UPLOAD_FILES_DIRECTORY;
            if (!Directory.Exists(fileDirectory)) {
                Directory.CreateDirectory(fileDirectory);
            }

            string filePath = Path.Combine(fileDirectory, Path.GetFileName(file.FileName));
            FileStream fileStream = new(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            fileStream.Close();
            return filePath;
        }

        public async static Task<List<string>> ReadAllLinesFromFileAsync(string filePath)
        {
            List<string> result = new();

            await foreach (string line in File.ReadLinesAsync(filePath)) {
                result.Add(line.Replace("\"", string.Empty));
            }

            return result;
        }

        public static string GetFileOwnerAndRemoveFile(string filePath)
        {
            if (!OperatingSystem.IsWindows()) {
                return Environment.UserName;
            }

            FileInfo fileInfo = new(filePath);
            FileSecurity fileSecurity = fileInfo.GetAccessControl();
            IdentityReference? identityReference = fileSecurity.GetOwner(typeof(NTAccount));
            fileInfo.Delete();

            return identityReference == null ? Environment.UserName : identityReference.Value;
        }
    }
}
