using Microsoft.AspNetCore.Http;

namespace Service.FileStrategy
{
    /// <summary>
    /// 文件操作抽象类
    /// </summary>
    public abstract class Strategy
    {
        /// <summary>
        /// 头像图片上传
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        public abstract Task<string> UploadFaceImage(List<IFormFile> formFiles);
    }
}
