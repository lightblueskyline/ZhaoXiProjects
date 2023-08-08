using Model.Enum;

namespace Service.FileStrategy
{
    /// <summary>
    /// 工厂类，负责创建对象
    /// </summary>
    public class FileFactory
    {
        public static Strategy CreateStrategy(UploadMode uploadMode)
        {
            switch (uploadMode)
            {
                case UploadMode.Local:
                    return new LocalStrategy();
                case UploadMode.Qiniu:
                    return new QiNiuStrategy();
                default:
                    return new LocalStrategy();
            }
        }
    }
}
