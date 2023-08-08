using Microsoft.AspNetCore.Http;

namespace Service.FileStrategy
{
    /// <summary>
    /// 本地策略
    /// </summary>
    public class LocalStrategy : Strategy
    {
        public override async Task<string> UploadFaceImage(List<IFormFile> formFiles)
        {
            var result = Task.Run(() =>
            {
                List<string> listResult = new List<string>();
                foreach (var file in formFiles)
                {
                    var filePath = $"{AppContext.BaseDirectory}/wwwroot/FaceImages";
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmssffff}_{file.FileName}";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var stream = File.Create($"{filePath}/{fileName}"))
                    {
                        file.CopyTo(stream);
                    }
                    listResult.Add(fileName);
                }
                // 将路径数组格式化成逗号分隔的字符串
                return string.Join(",", listResult);
            });

            return await result;
        }
    }
}
