using Interface;
using Microsoft.AspNetCore.Http;
using Model.Enum;
using Service.FileStrategy;

namespace Service
{
    public class FileService : IFileService
    {
        public async Task<string> Upload(List<IFormFile> formFiles, UploadMode mode)
        {
            FileContext fileContext = new FileContext(FileFactory.CreateStrategy(mode), formFiles);
            return await fileContext.ContextInterface();
        }
    }
}
