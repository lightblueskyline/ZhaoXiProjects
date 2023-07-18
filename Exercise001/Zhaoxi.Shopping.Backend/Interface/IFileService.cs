using Microsoft.AspNetCore.Http;
using Model.Enum;

namespace Interface
{
    public interface IFileService
    {
        Task<string> Upload(List<IFormFile> formFiles, UploadMode mode);
    }
}
