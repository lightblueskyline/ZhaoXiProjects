using Microsoft.AspNetCore.Http;

namespace Service.FileStrategy
{
    /// <summary>
    /// 上下文，通过此类以调用具体策略
    /// </summary>
    public class FileContext
    {
        private Strategy _Strategy;
        private List<IFormFile> _IFormFiles;

        public FileContext(Strategy strategy, List<IFormFile> formFiles)
        {
            _Strategy = strategy;
            _IFormFiles = formFiles;
        }

        public async Task<string> ContextInterface()
        {
            return await _Strategy.UploadFaceImage(_IFormFiles);
        }
    }
}
