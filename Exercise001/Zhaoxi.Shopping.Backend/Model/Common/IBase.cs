using SqlSugar;

namespace Model.Common
{
    public class IBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string ID { get; set; }
    }
}
