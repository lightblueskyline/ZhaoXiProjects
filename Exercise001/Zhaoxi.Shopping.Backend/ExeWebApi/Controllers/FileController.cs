using ExeWebApi.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Other;

namespace ExeWebApi.Controllers
{
    public class FileController : MyBaseController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="files">文件对象</param>
        /// <param name="mode">本地、七牛云...</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UploadFile(List<IFormFile> files, UploadMode mode)
        {
            return ResultHelper.Success(await _fileService.Upload(files, mode));
        }
    }
}
