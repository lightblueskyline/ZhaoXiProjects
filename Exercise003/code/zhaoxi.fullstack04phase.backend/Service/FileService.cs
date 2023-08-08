using Interface;
using Microsoft.AspNetCore.Http;
using Model.Enum;
using Service.FileStrategy;

namespace Service
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFaceImage(List<IFormFile> formFiles, UploadMode uploadMode)
        {
            FileContext fileContext = new FileContext(FileFactory.CreateStrategy(uploadMode), formFiles);
            return await fileContext.ContextInterface();
        }
    }
}
