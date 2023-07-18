using Model.Enum;

namespace Service.FileStrategy
{
    /// <summary>
    /// 工厂类，负责对象的实例化
    /// </summary>
    public class FileFactory
    {
        public static Strategy CreateStrategy(UploadMode mode)
        {
            switch (mode)
            {
                case UploadMode.QiNiu: return new QiNiuCloudStrategy();
                case UploadMode.Local: return new LocalStrategy();
                default: return new LocalStrategy();
            }
        }
    }
}
