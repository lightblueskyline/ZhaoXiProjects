using ExecWebAPI.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Other;

namespace ExecWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _IFileService;

        public FileController(IFileService fileService)
        {
            _IFileService = fileService;
        }

        [HttpPost]
        public async Task<ApiResult> UploadFaceImage(List<IFormFile> formFiles, UploadMode uploadMode)
        {
            return ResultHelper.Success(await _IFileService.UploadFaceImage(formFiles, uploadMode));
        }
    }
}
