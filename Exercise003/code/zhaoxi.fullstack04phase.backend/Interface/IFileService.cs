using Microsoft.AspNetCore.Http;
using Model.Enum;

namespace Interface
{
    public interface IFileService
    {
        Task<string> UploadFaceImage(List<IFormFile> formFiles, UploadMode uploadMode);
    }
}
