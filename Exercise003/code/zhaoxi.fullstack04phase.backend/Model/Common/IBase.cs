using SqlSugar;

namespace Model.Common
{
    public class IBase
    {
        /// <summary>
        /// 主键
        /// 使用 GUID 类型修改为 String
        /// </summary>
        //[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
    }
}
