using Microsoft.AspNetCore.Http;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;

namespace Service.FileStrategy
{
    /// <summary>
    /// 七牛云上传
    /// </summary>
    public class QiNiuCloudStrategy : Strategy
    {
        public override async Task<string> Upload(List<IFormFile> formFiles)
        {
            var result = await Task.Run(() =>
            {
                Mac mac = new Mac("AccessKey", "SecretKey");
                List<string> files = new List<string>();
                foreach (var file in formFiles)
                {
                    if (file.Length > 0)
                    {
                        var filePath = $"{AppContext.BaseDirectory}/wwwroot/tempImages";
                        var fileName = $"/{DateTime.Now:yyyyMMddHHmmssffff}{file.FileName}";
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        using (var stream = System.IO.File.Create(filePath + fileName))
                        {
                            file.CopyTo(stream);
                        }
                        // 上传文件名
                        string key = fileName;
                        // 本地文件路径
                        string localFilePath = $"{filePath}{fileName}";
                        // 存储空间名
                        string Bucket = "pl-static";
                        // 设置上传策略
                        PutPolicy putPolicy = new PutPolicy();
                        // 设置上传的目标空间
                        putPolicy.Scope = Bucket;
                        // 上传策略的过期时间 (秒)
                        // putPolicy.SetExpires(3600);
                        // 上传完毕后，指定天数之后自动删除
                        // putPolicy.DeleteAfterDays = 1;
                        // 生成 Token
                        string tokenQiNiu = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
                        Config configQiNiu = new Config();
                        // 设置上传区域
                        configQiNiu.Zone = Zone.ZONE_CN_North;
                        // 设置 HTTP / HTTPS
                        configQiNiu.UseHttps = true;
                        configQiNiu.UseCdnDomains = true;
                        configQiNiu.ChunkSize = ChunkUnit.U512K;
                        // 表单上传
                        FormUploader formUploader = new FormUploader(configQiNiu);
                        HttpResult httpResult = formUploader.UploadFile(localFilePath, key, tokenQiNiu, null);
                        files.Add(fileName);
                        // 删除本地文件夹
                        Directory.Delete(filePath, true);
                    }
                }

                return String.Join(",", files);
            });

            return "";
        }
    }
}
