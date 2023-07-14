using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 日志表
    /// </summary>
    public class Log4Net
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        [SugarColumn(IsNullable = false)]
        public DateTime Date { get; set; } = DateTime.Now;

        [SugarColumn(IsNullable = false)]
        public string Thread { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Level { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Logger { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Message { get; set; }

        public string Exception { get; set; }
    }
}
