using Microsoft.AspNetCore.Http;

namespace Service.FileStrategy
{
    /// <summary>
    /// 本地上传
    /// </summary>
    public class LocalStrategy : Strategy
    {
        public override async Task<string> Upload(List<IFormFile> formFiles)
        {
            var result = await Task.Run(() =>
            {
                List<string> files = new List<string>();
                foreach (var file in formFiles)
                {
                    if (file.Length > 0)
                    {
                        var filePath = $"{AppContext.BaseDirectory}/wwwroot";
                        var fileName = $"/{DateTime.Now:yyyyMMddHHmmssffff}{file.FileName}";
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        using (var stream = System.IO.File.Create(filePath + fileName))
                        {
                            file.CopyTo(stream);
                        }
                        files.Add(fileName);
                    }
                }
                return String.Join(",", files);
            });
            return result;
        }
    }
}
