using Microsoft.AspNetCore.Http;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;

namespace Service.FileStrategy
{
    /// <summary>
    /// 七牛云
    /// </summary>
    public class QiNiuStrategy : Strategy
    {
        public override async Task<string> UploadFaceImage(List<IFormFile> formFiles)
        {
            var result = Task.Run(() =>
            {
                Mac mac = new Mac("accessKey", "secretKey");
                List<string> listResult = new List<string>();
                foreach (var file in formFiles)
                {
                    if (file.Length > 0)
                    {
                        var tempFilePath = $"{AppContext.BaseDirectory}/wwwroot/FaceImages";
                        var fileName = $"{DateTime.Now:yyyyMMddHHmmssffff}_{file.FileName}";
                        if (!Directory.Exists(tempFilePath))
                        {
                            Directory.CreateDirectory(tempFilePath);
                        }
                        using (var stream = File.Create($"{tempFilePath}/{fileName}"))
                        {
                            file.CopyTo(stream);
                        }
                        // 上传文件名
                        string key = fileName;
                        // 本地文件路径
                        string localFilePath = $"{tempFilePath}/{fileName}";
                        // 存储空间名
                        string Bucket = "pl-static";
                        // 设置上传策略
                        PutPolicy putPolicy = new PutPolicy();
                        // 设置上传的目标空间
                        putPolicy.Scope = Bucket;
                        // 上传策略的过期时间(单位： 秒)
                        // putPolicy.SetExpires(3600);
                        // 文件上传完毕后，在多少天后自动被删除
                        // putPolicy.DeleteAfterDays = 1;
                        // 生成上传 Token
                        string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
                        Config config = new Config();
                        // 设置上传区域
                        config.Zone = Zone.ZONE_CN_East;
                        // 设置 Http / Https 上传
                        config.UseHttps = true;
                        config.UseCdnDomains = true;
                        config.ChunkSize = ChunkUnit.U512K;
                        // 表单上传
                        FormUploader target = new FormUploader(config);
                        HttpResult result = target.UploadFile(localFilePath, key, token, null);
                        listResult.Add(fileName);
                        // 删除临时文件夹
                        Directory.Delete(tempFilePath);
                    }
                }
                return String.Join(",", listResult);
            });

            return await result;
        }
    }
}
